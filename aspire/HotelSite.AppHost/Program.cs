var builder = DistributedApplication.CreateBuilder(args);

var aoaiEndpoint = builder.Configuration["AzureOpenAI:Endpoint"] ?? "";
var aoaiKey = builder.Configuration["AzureOpenAI:ApiKey"] ?? "";
var aoaiDeployment = builder.Configuration["AzureOpenAI:DeploymentName"] ?? "gpt-4o";

var api = builder.AddProject<Projects.HotelBooking_Api>("api")
    .WithEnvironment("AZURE_OPENAI_ENDPOINT", aoaiEndpoint)
    .WithEnvironment("AZURE_OPENAI_API_KEY", aoaiKey)
    .WithEnvironment("AZURE_OPENAI_DEPLOYMENT_NAME", aoaiDeployment);

var frontend = builder.AddViteApp("frontend", "../../frontend")
    .WithNpm()
    .WithReference(api)
    .WaitFor(api)
    .WithEndpoint("http", (endpointAnnotation) =>
    {
        endpointAnnotation.Port = 5173;
    })
    .WithEnvironment("PORT", "5173")
    .WithExternalHttpEndpoints()
    .WithEnvironment("VITE_OTEL_EXPORTER_OTLP_ENDPOINT", "/otel")
    .WithEnvironment("DOTNET_DASHBOARD_OTLP_HTTP_ENDPOINT_URL", builder.Configuration["DOTNET_DASHBOARD_OTLP_HTTP_ENDPOINT_URL"] ?? "http://localhost:19171");

builder.Build().Run();
