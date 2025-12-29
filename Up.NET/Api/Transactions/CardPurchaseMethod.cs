using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Up.NET.Api.Transactions;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum CardPurchaseMethodType
{
    [EnumMember(Value = "BAR_CODE")]
    BarCode,

    [EnumMember(Value = "OCR")]
    Ocr,

    [EnumMember(Value = "CARD_PIN")]
    CardPin,

    [EnumMember(Value = "CARD_DETAILS")]
    CardDetails,

    [EnumMember(Value = "CARD_ON_FILE")]
    CardOnFile,

    [EnumMember(Value = "ECOMMERCE")]
    Ecommerce,

    [EnumMember(Value = "MAGNETIC_STRIPE")]
    MagneticStripe,

    [EnumMember(Value = "CONTACTLESS")]
    Contactless
}

public class CardPurchaseMethod
{
    public CardPurchaseMethodType Method { get; set; }
    public string CardNumberSuffix { get; set; }
}
