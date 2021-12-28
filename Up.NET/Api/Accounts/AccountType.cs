using System.Text.Json.Serialization;

namespace Up.NET.Api.Accounts;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum AccountType
{
    Saver,
    Transactional
}