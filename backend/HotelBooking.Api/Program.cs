using System.ClientModel;
using Azure.AI.OpenAI;
using HotelBooking.Api.AgentTools;
using HotelBooking.Api.McpTools;
using HotelBooking.Api.Models;
using HotelBooking.Api.Services;
using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Hosting.AGUI.AspNetCore;
using Microsoft.Extensions.AI;

var builder = WebApplication.CreateBuilder(args);

// Aspire ServiceDefaults (telemetry, health checks, service discovery)
builder.AddServiceDefaults();

// Services (singleton for in-memory data)
builder.Services.AddSingleton<HotelService>();
builder.Services.AddSingleton<BookingService>();
builder.Services.AddSingleton<FeatureToggleService>();

// OpenAPI
builder.Services.AddOpenApi();

// CORS (allow frontend dev server)
builder.Services.AddCors(o => o.AddDefaultPolicy(b =>
    b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

// MCP Server
builder.Services.AddMcpServer()
    .WithHttpTransport()
    .WithTools<HotelTools>();

// AG-UI
builder.Services.AddHttpClient().AddLogging();
builder.Services.AddAGUI();

var app = builder.Build();

app.UseCors();
app.MapOpenApi();
app.MapDefaultEndpoints();

// Serve the React frontend as static files (production)
app.UseDefaultFiles();
app.UseStaticFiles();

// ──────────────────────────────────────────────
//  REST API: Hotels
// ──────────────────────────────────────────────
var hotelsApi = app.MapGroup("/api/hotels").WithTags("Hotels");

hotelsApi.MapGet("/", (HotelService svc, string? city, string? country, int? minStars, decimal? maxPrice, int? guests) =>
    svc.Search(new HotelSearchRequest(city, country, null, null, guests, minStars, maxPrice)))
    .WithName("SearchHotels")
    .WithDescription("Search hotels with optional filters");

hotelsApi.MapGet("/{id}", (HotelService svc, string id) =>
    svc.GetById(id) is { } hotel ? Results.Ok(hotel) : Results.NotFound())
    .WithName("GetHotel")
    .WithDescription("Get hotel details by ID");

hotelsApi.MapGet("/{id}/rooms", (HotelService svc, string id, int? guests) =>
    svc.GetAvailableRooms(id, guests))
    .WithName("GetAvailableRooms")
    .WithDescription("Get available rooms for a hotel");

// ──────────────────────────────────────────────
//  REST API: Bookings
// ──────────────────────────────────────────────
var bookingsApi = app.MapGroup("/api/bookings").WithTags("Bookings");

bookingsApi.MapPost("/", (BookingService svc, CreateBookingRequest req) =>
    svc.Create(req) is { } confirmation ? Results.Created($"/api/bookings/{confirmation.BookingId}", confirmation) : Results.BadRequest("Invalid booking request"))
    .WithName("CreateBooking")
    .WithDescription("Create a new booking");

bookingsApi.MapGet("/{id}", (BookingService svc, string id) =>
    svc.GetById(id) is { } booking ? Results.Ok(booking) : Results.NotFound())
    .WithName("GetBooking")
    .WithDescription("Get booking by ID");

bookingsApi.MapGet("/by-email/{email}", (BookingService svc, string email) =>
    svc.GetByEmail(email))
    .WithName("GetBookingsByEmail")
    .WithDescription("List bookings for a guest email");

bookingsApi.MapDelete("/{id}", (BookingService svc, string id) =>
    svc.Cancel(id) ? Results.Ok("Cancelled") : Results.NotFound())
    .WithName("CancelBooking")
    .WithDescription("Cancel a booking");

// ──────────────────────────────────────────────
//  REST API: Feature Toggles
// ──────────────────────────────────────────────
var configApi = app.MapGroup("/api/config").WithTags("Configuration");

configApi.MapGet("/features", (FeatureToggleService svc) => svc.GetConfig())
    .WithName("GetFeatures")
    .WithDescription("Get feature toggle configuration");

configApi.MapPut("/features/chat", (FeatureToggleService svc, bool enabled) =>
{
    svc.SetChatEnabled(enabled);
    return Results.Ok(svc.GetConfig());
})
    .WithName("SetChatEnabled")
    .WithDescription("Enable or disable the chat feature");

// ──────────────────────────────────────────────
//  MCP Server (SSE transport at /mcp)
// ──────────────────────────────────────────────
app.MapMcp("/mcp");

// ──────────────────────────────────────────────
//  AG-UI Agent endpoint (MAF)
// ──────────────────────────────────────────────
var endpoint = builder.Configuration["AZURE_OPENAI_ENDPOINT"];
var apiKey = builder.Configuration["AZURE_OPENAI_API_KEY"];
var deploymentName = builder.Configuration["AZURE_OPENAI_DEPLOYMENT_NAME"] ?? "gpt-4o";

if (!string.IsNullOrEmpty(endpoint) && !string.IsNullOrEmpty(apiKey) && Uri.TryCreate(endpoint, UriKind.Absolute, out var endpointUri))
{
    var hotelService = app.Services.GetRequiredService<HotelService>();
    var bookingService = app.Services.GetRequiredService<BookingService>();
    var tools = HotelAgentTools.Create(hotelService, bookingService);

    var chatClient = new AzureOpenAIClient(endpointUri, new ApiKeyCredential(apiKey))
        .GetChatClient(deploymentName);

    var agent = chatClient.AsIChatClient().AsAIAgent(
        name: "HotelBookingAssistant",
        instructions: """
            You are a helpful hotel booking assistant. You can help users:
            - Search for hotels by city, country, star rating, price, or guest count
            - View hotel details and available rooms
            - Make bookings
            - Check existing bookings
            - Cancel bookings
            Be friendly, concise, and proactive in suggesting options.
            When presenting hotels, include key details like name, location, rating, and starting price.
            Always confirm booking details before creating a reservation.
            """,
        tools: tools);

    app.MapAGUI("/agent-chat", agent);
    app.Logger.LogInformation("AG-UI agent endpoint enabled at /agent-chat");
}
else
{
    app.Logger.LogWarning("AZURE_OPENAI_ENDPOINT or AZURE_OPENAI_API_KEY not set — AG-UI agent chat disabled.");
}

// SPA fallback — serve index.html for any unmatched routes (must be after all API routes)
app.MapFallbackToFile("index.html");

app.Run();
