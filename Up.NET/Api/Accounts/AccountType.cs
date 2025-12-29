using System.Text.Json.Serialization;

namespace Up.NET.Api.Accounts;

[JsonConverter(typeof(JsonStringEnumConverter<AccountType>))]
public enum AccountType
{
    [JsonStringEnumMemberName("SAVER")]
    Saver,

    [JsonStringEnumMemberName("TRANSACTIONAL")]
    Transactional,

    [JsonStringEnumMemberName("HOME_LOAN")]
    HomeLoan
}
