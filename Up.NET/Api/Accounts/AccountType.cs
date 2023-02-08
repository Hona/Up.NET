using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Up.NET.Api.Accounts;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum AccountType
{
    Saver,
    Transactional,
    [EnumMember(Value = "HOME_LOAN")] HomeLoan
}