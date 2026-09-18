using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Latchkey;
using ModelContextProtocol.Server;
using Up.NET.Api;
using Up.NET.Mcp;

if (args.Length > 0)
{
    Environment.ExitCode = await AuthCommand.RunAsync(args);
    return;
}

var tokenStore = new AccessTokenStore();
string? accessToken;
try
{
    accessToken = tokenStore.GetToken();
}
catch (LatchkeyBackendUnavailableException exception)
{
    Console.Error.WriteLine($"The operating system credential store is unavailable: {exception.Message}");
    Console.Error.WriteLine("For headless systems, inject UP_ACCESS_TOKEN from a secret manager instead.");
    Environment.ExitCode = 1;
    return;
}
catch (LatchkeyException exception)
{
    Console.Error.WriteLine($"Credential store error: {exception.Message}");
    Environment.ExitCode = 1;
    return;
}

if (string.IsNullOrWhiteSpace(accessToken))
{
    Console.Error.WriteLine("No Up token found. Run 'up-mcp auth login' or set UP_ACCESS_TOKEN.");
    Environment.ExitCode = 1;
    return;
}

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole(options => options.LogToStandardErrorThreshold = LogLevel.Trace);
builder.Logging.SetMinimumLevel(LogLevel.Warning);

builder.Services.AddSingleton<IUpApi>(new UpApi(accessToken));

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<UpTools>();

await builder.Build().RunAsync();
