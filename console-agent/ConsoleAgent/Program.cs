using System.ClientModel;
using Azure.AI.OpenAI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using ModelContextProtocol.Client;

// ── Configuration ──
// Find the solution root by walking up from the source directory, then load AppHost appsettings
static string FindSolutionRoot()
{
    // Start from the source file's directory (works regardless of build output depth)
    var dir = new DirectoryInfo(AppContext.BaseDirectory);
    while (dir is not null)
    {
        if (dir.GetFiles("hotelsite.sln").Length > 0)
            return dir.FullName;
        dir = dir.Parent;
    }
    throw new InvalidOperationException("Could not find hotelsite.sln in any parent directory.");
}

var solutionRoot = FindSolutionRoot();
var appHostPath = Path.Combine(solutionRoot, "aspire", "HotelSite.AppHost");
var config = new ConfigurationBuilder()
    .AddJsonFile(Path.Combine(appHostPath, "appsettings.json"), optional: true)
    .AddJsonFile(Path.Combine(appHostPath, "appsettings.Development.json"), optional: true)
    .AddEnvironmentVariables()
    .Build();

var endpoint = config["AzureOpenAI:Endpoint"]
    ?? Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT")
    ?? throw new InvalidOperationException(
        "Set AzureOpenAI:Endpoint in AppHost appsettings.json or AZURE_OPENAI_ENDPOINT env var");
var apiKey = config["AzureOpenAI:ApiKey"]
    ?? Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY")
    ?? throw new InvalidOperationException(
        "Set AzureOpenAI:ApiKey in AppHost appsettings.json or AZURE_OPENAI_API_KEY env var");
var deploymentName = config["AzureOpenAI:DeploymentName"]
    ?? Environment.GetEnvironmentVariable("AZURE_OPENAI_DEPLOYMENT_NAME")
    ?? "gpt-4o";
var mcpUrl = Environment.GetEnvironmentVariable("MCP_SERVER_URL") ?? "http://localhost:5000/mcp";

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("╔══════════════════════════════════════════════════╗");
Console.WriteLine("║      🏨  Hotel Booking Console Agent            ║");
Console.WriteLine("║      Powered by Microsoft Agent Framework       ║");
Console.WriteLine("╚══════════════════════════════════════════════════╝");
Console.ResetColor();
Console.WriteLine($"\nConnecting to MCP server at {mcpUrl}...");

// ── Connect to MCP Server via HTTP ──
await using var mcpClient = await McpClient.CreateAsync(
    new HttpClientTransport(new HttpClientTransportOptions
    {
        Endpoint = new Uri(mcpUrl),
    }));

var mcpTools = await mcpClient.ListToolsAsync();
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine($"✓ Connected! {mcpTools.Count} tools available:");
foreach (var tool in mcpTools)
{
    Console.WriteLine($"  • {tool.Name}: {tool.Description}");
}
Console.ResetColor();

// ── Create AI Chat Client with function invocation ──
IChatClient chatClient =
    new ChatClientBuilder(
        new AzureOpenAIClient(new Uri(endpoint), new ApiKeyCredential(apiKey))
        .GetChatClient(deploymentName).AsIChatClient())
    .UseFunctionInvocation()
    .Build();

// ── Interactive Chat Loop ──
Console.WriteLine("\nType your message (or 'quit' to exit):\n");

List<ChatMessage> messages =
[
    new(ChatRole.System, """
        You are a helpful hotel booking assistant running in a console application.
        You have access to a hotel booking system via MCP tools.
        You can search hotels, view details, check available rooms, create bookings,
        look up existing bookings, and cancel bookings.
        Be concise but informative. Format responses for terminal readability.
        Use bullet points and clear structure.
        """)
];

while (true)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("You > ");
    Console.ResetColor();

    var input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input) || input.Equals("quit", StringComparison.OrdinalIgnoreCase))
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\nGoodbye! 👋");
        Console.ResetColor();
        break;
    }

    messages.Add(new(ChatRole.User, input));

    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.Write("\n🤖 Assistant > ");
    Console.ResetColor();

    try
    {
        List<ChatResponseUpdate> updates = [];
        await foreach (var update in chatClient.GetStreamingResponseAsync(
            messages, new() { Tools = [.. mcpTools] }))
        {
            Console.Write(update);
            updates.Add(update);
        }
        messages.AddMessages(updates);
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"Error: {ex.Message}");
        Console.ResetColor();
    }

    Console.WriteLine("\n");
}
