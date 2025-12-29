using System.Text.Json.Serialization;

namespace Up.NET.Api.Transactions;

[JsonConverter(typeof(JsonStringEnumConverter<CardPurchaseMethodType>))]
public enum CardPurchaseMethodType
{
    [JsonStringEnumMemberName("BAR_CODE")]
    BarCode,

    [JsonStringEnumMemberName("OCR")]
    Ocr,

    [JsonStringEnumMemberName("CARD_PIN")]
    CardPin,

    [JsonStringEnumMemberName("CARD_DETAILS")]
    CardDetails,

    [JsonStringEnumMemberName("CARD_ON_FILE")]
    CardOnFile,

    [JsonStringEnumMemberName("ECOMMERCE")]
    Ecommerce,

    [JsonStringEnumMemberName("MAGNETIC_STRIPE")]
    MagneticStripe,

    [JsonStringEnumMemberName("CONTACTLESS")]
    Contactless
}

public class CardPurchaseMethod
{
    public CardPurchaseMethodType Method { get; set; }
    public string CardNumberSuffix { get; set; }
}
