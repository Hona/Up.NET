using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using Up.NET.Api;
using Up.NET.Mcp;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole(options => options.LogToStandardErrorThreshold = LogLevel.Trace);
builder.Logging.SetMinimumLevel(LogLevel.Warning);

builder.Services.AddSingleton<IUpApi>(_ =>
{
    var accessToken = Environment.GetEnvironmentVariable("UP_ACCESS_TOKEN");
    if (string.IsNullOrWhiteSpace(accessToken))
    {
        throw new InvalidOperationException("Set the UP_ACCESS_TOKEN environment variable before starting the server.");
    }

    return new UpApi(accessToken);
});

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<UpTools>();

await builder.Build().RunAsync();
