namespace Up.NET.Api.Accounts
{
    public class MoneyObject
    {
        /// <summary>
        /// ISO 4217 currency code
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Localized string to the currency
        /// </summary>
        public string Value { get; set; }

        public long ValueInBaseUnits { get; set; }
    }
}