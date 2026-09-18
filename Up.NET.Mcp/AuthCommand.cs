using System.Text;
using Latchkey;
using Up.NET.Api;
using Up.NET.Models;

namespace Up.NET.Mcp;

internal static class AuthCommand
{
    public static async Task<int> RunAsync(string[] args)
    {
        if (args is ["--help" or "-h" or "help"])
        {
            PrintHelp();
            return 0;
        }

        if (args is not ["auth", var operation])
        {
            Console.Error.WriteLine("Unknown command. Run 'up-mcp --help' for usage.");
            return 2;
        }

        try
        {
            var tokenStore = new AccessTokenStore();
            return operation.ToLowerInvariant() switch
            {
                "login" => await LoginAsync(tokenStore),
                "status" => Status(tokenStore),
                "logout" => Logout(tokenStore),
                _ => UnknownAuthCommand(operation)
            };
        }
        catch (LatchkeyBackendUnavailableException exception)
        {
            Console.Error.WriteLine($"The operating system credential store is unavailable: {exception.Message}");
            Console.Error.WriteLine("For headless systems, inject UP_ACCESS_TOKEN from a secret manager instead.");
            return 1;
        }
        catch (LatchkeyException exception)
        {
            Console.Error.WriteLine($"Credential store error: {exception.Message}");
            return 1;
        }
        catch (HttpRequestException exception)
        {
            Console.Error.WriteLine($"Could not contact Up to validate the token: {exception.Message}");
            return 1;
        }
        catch (TaskCanceledException)
        {
            Console.Error.WriteLine("The Up token validation request timed out.");
            return 1;
        }
    }

    private static async Task<int> LoginAsync(AccessTokenStore tokenStore)
    {
        if (Console.IsInputRedirected)
        {
            Console.Error.WriteLine("Login requires an interactive terminal so the token can be entered securely.");
            return 1;
        }

        Console.Write("Up personal access token: ");
        var token = ReadSecret();
        Console.WriteLine();

        if (string.IsNullOrWhiteSpace(token))
        {
            Console.Error.WriteLine("A token is required.");
            return 1;
        }

        token = token.Trim();
        Console.WriteLine("Validating token with Up...");
        var response = await new UpApi(token).GetPingAsync();
        if (!response.Success)
        {
            Console.Error.WriteLine(FormatErrors(response.Errors));
            return 1;
        }

        tokenStore.Save(token);
        if (!tokenStore.HasStoredToken)
        {
            Console.Error.WriteLine("The credential store did not persist the token.");
            return 1;
        }

        Console.WriteLine($"Token stored securely. Authenticated as {response.Response.Meta.Id}.");
        return 0;
    }

    private static int Status(AccessTokenStore tokenStore)
    {
        if (tokenStore.HasEnvironmentToken)
        {
            Console.WriteLine("Authenticated with UP_ACCESS_TOKEN.");
            return 0;
        }

        if (tokenStore.HasStoredToken)
        {
            Console.WriteLine("Authenticated with the operating system credential store.");
            return 0;
        }

        Console.WriteLine("Not authenticated. Run 'up-mcp auth login'.");
        return 1;
    }

    private static int Logout(AccessTokenStore tokenStore)
    {
        var removed = tokenStore.Delete();
        Console.WriteLine(removed ? "Stored token removed." : "No stored token was found.");

        if (tokenStore.HasEnvironmentToken)
        {
            Console.WriteLine("UP_ACCESS_TOKEN is still set and takes precedence over stored credentials.");
        }

        return 0;
    }

    private static string ReadSecret()
    {
        var value = new StringBuilder();
        while (true)
        {
            var key = Console.ReadKey(intercept: true);
            if (key.Key == ConsoleKey.Enter)
            {
                return value.ToString();
            }

            if (key.Key == ConsoleKey.Backspace)
            {
                if (value.Length > 0)
                {
                    value.Length--;
                }

                continue;
            }

            if (!char.IsControl(key.KeyChar))
            {
                value.Append(key.KeyChar);
            }
        }
    }

    private static string FormatErrors(IEnumerable<ErrorResponse>? errors)
    {
        var messages = errors?.Select(error => $"{(int)error.Status} {error.Title}: {error.Detail}").ToArray();
        return messages is { Length: > 0 }
            ? $"Up rejected the token: {string.Join("; ", messages)}"
            : "Up rejected the token.";
    }

    private static int UnknownAuthCommand(string operation)
    {
        Console.Error.WriteLine($"Unknown auth command '{operation}'. Use login, status, or logout.");
        return 2;
    }

    private static void PrintHelp()
    {
        Console.WriteLine("""
            Up.NET MCP server

            Usage:
              up-mcp                 Start the MCP server over stdio
              up-mcp auth login      Validate and securely store an Up token
              up-mcp auth status     Show whether authentication is configured
              up-mcp auth logout     Remove the stored token

            UP_ACCESS_TOKEN overrides stored credentials for CI and headless systems.
            """);
    }
}
