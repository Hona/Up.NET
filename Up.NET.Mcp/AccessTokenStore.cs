using Latchkey;

namespace Up.NET.Mcp;

internal sealed class AccessTokenStore
{
    private const string EnvironmentVariable = "UP_ACCESS_TOKEN";
    private const string ServiceName = "com.github.hona.up-net-mcp";
    private const string TokenKey = "personal-access-token";

    private readonly Lazy<ILatchkey> _keyring = new(() => LatchkeyFactory.Create(new LatchkeyOptions
    {
        ServiceName = ServiceName,
        DisplayName = "Up.NET MCP"
    }));

    public string? GetToken()
    {
        var environmentToken = GetEnvironmentToken();
        return environmentToken ?? _keyring.Value.Get(TokenKey);
    }

    public bool HasEnvironmentToken => GetEnvironmentToken() is not null;

    public bool HasStoredToken => _keyring.Value.Contains(TokenKey);

    public void Save(string token) => _keyring.Value.Set(TokenKey, token);

    public bool Delete() => _keyring.Value.Delete(TokenKey);

    private static string? GetEnvironmentToken()
    {
        var token = Environment.GetEnvironmentVariable(EnvironmentVariable);
        return string.IsNullOrWhiteSpace(token) ? null : token.Trim();
    }
}
