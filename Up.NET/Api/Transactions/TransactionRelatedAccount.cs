using System;

namespace Up.NET.Api.Transactions
{
    public class TransactionRelatedAccount
    {
        [Obsolete("Always `accounts`")]
        public string Type { get; set; }

        public string Id { get; set; }
    }
}