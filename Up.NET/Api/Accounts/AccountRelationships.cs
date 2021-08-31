using System.Text.Json.Serialization;
using Up.NET.Models;

namespace Up.NET.Api.Accounts
{
    public class AccountRelationships
    {
        public AccountTransactions Transactions { get; set; }

    }

}