using System.Text.Json.Serialization;
using Up.NET.Api.Accounts;
using Up.NET.Api.Attachments;
using Up.NET.Api.Categories;
using Up.NET.Api.Tags;
using Up.NET.Api.Transactions;
using Up.NET.Api.Utilities;
using Up.NET.Api.Webhooks;
using Up.NET.Api.Webhooks.Events;
using Up.NET.Api.Webhooks.Logs;
using Up.NET.Models;

namespace Up.NET;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    PropertyNameCaseInsensitive = true)]
// Models
[JsonSerializable(typeof(ErrorWrapper))]
[JsonSerializable(typeof(ErrorResponse))]
[JsonSerializable(typeof(ErrorSource))]
[JsonSerializable(typeof(NoResponse))]
[JsonSerializable(typeof(PaginatedLinks))]
[JsonSerializable(typeof(RelatedLink))]
[JsonSerializable(typeof(SelfLink))]
// Accounts
[JsonSerializable(typeof(AccountResource))]
[JsonSerializable(typeof(AccountAttributes))]
[JsonSerializable(typeof(AccountRelationships))]
[JsonSerializable(typeof(AccountTransactions))]
[JsonSerializable(typeof(MoneyObject))]
[JsonSerializable(typeof(AccountType))]
[JsonSerializable(typeof(OwnershipType))]
// Attachments
[JsonSerializable(typeof(AttachmentResource))]
[JsonSerializable(typeof(AttachmentAttributes))]
[JsonSerializable(typeof(AttachmentRelationships))]
[JsonSerializable(typeof(AttachmentRelated))]
[JsonSerializable(typeof(AttachmentRelatedTransaction))]
// Categories
[JsonSerializable(typeof(CategoriesResource))]
[JsonSerializable(typeof(CategoriesAttributes))]
[JsonSerializable(typeof(CategoriesRelationships))]
[JsonSerializable(typeof(CategoriesChildren))]
[JsonSerializable(typeof(CategoriesParent))]
[JsonSerializable(typeof(CategoriesRelated))]
[JsonSerializable(typeof(CategoryInputResourceIdentifier))]
// Tags
[JsonSerializable(typeof(TagResource))]
[JsonSerializable(typeof(TagRelationships))]
[JsonSerializable(typeof(TagTransactions))]
[JsonSerializable(typeof(TagInputResourceIdentifier))]
// Transactions
[JsonSerializable(typeof(TransactionResource))]
[JsonSerializable(typeof(TransactionAttributes))]
[JsonSerializable(typeof(TransactionRelationships))]
[JsonSerializable(typeof(TransactionRelatedAccount))]
[JsonSerializable(typeof(TransactionStatus))]
[JsonSerializable(typeof(CardPurchaseMethod))]
[JsonSerializable(typeof(CardPurchaseMethodType))]
[JsonSerializable(typeof(Cashback))]
[JsonSerializable(typeof(CustomerObject))]
[JsonSerializable(typeof(HoldInfo))]
[JsonSerializable(typeof(NoteObject))]
[JsonSerializable(typeof(RoundUp))]
// Utilities
[JsonSerializable(typeof(PingResponse))]
[JsonSerializable(typeof(PingMeta))]
// Webhooks
[JsonSerializable(typeof(WebhookResource))]
[JsonSerializable(typeof(WebhookAttributes))]
[JsonSerializable(typeof(WebhookRelationships))]
[JsonSerializable(typeof(WebhookRelatedLogs))]
[JsonSerializable(typeof(WebhookInputResource))]
[JsonSerializable(typeof(WebhookInputAttributes))]
// Webhook Events
[JsonSerializable(typeof(WebhookEventResource))]
[JsonSerializable(typeof(WebhookEventAttributes))]
[JsonSerializable(typeof(WebhookEventRelationships))]
[JsonSerializable(typeof(WebhookEventRelatedTransaction))]
[JsonSerializable(typeof(WebhookEventRelatedTransactionData))]
[JsonSerializable(typeof(WebhookEventRelatedWebhook))]
[JsonSerializable(typeof(WebhookEventRelatedWebhookData))]
[JsonSerializable(typeof(WebhookEventType))]
// Webhook Logs
[JsonSerializable(typeof(WebhookDeliveryLogResource))]
[JsonSerializable(typeof(WebhookDeliveryLogAttributes))]
[JsonSerializable(typeof(WebhookDeliveryLogRelationships))]
[JsonSerializable(typeof(WebhookDeliveryLogRelatedWebhook))]
[JsonSerializable(typeof(WebhookDeliveryLogRelatedWebhookData))]
[JsonSerializable(typeof(WebhookDeliveryLogRequest))]
[JsonSerializable(typeof(WebhookDeliveryLogResponse))]
[JsonSerializable(typeof(WebhookDeliveryStatus))]
// Closed generic types - DataResponse<T>
[JsonSerializable(typeof(DataResponse<AccountResource>))]
[JsonSerializable(typeof(DataResponse<AttachmentResource>))]
[JsonSerializable(typeof(DataResponse<CategoriesResource>))]
[JsonSerializable(typeof(DataResponse<List<CategoriesResource>>))]
[JsonSerializable(typeof(DataResponse<TransactionResource>))]
[JsonSerializable(typeof(DataResponse<WebhookResource>))]
[JsonSerializable(typeof(DataResponse<WebhookEventResource>))]
[JsonSerializable(typeof(DataResponse<List<TagInputResourceIdentifier>>))]
// Closed generic types - DataWrapper<T>
[JsonSerializable(typeof(DataWrapper<WebhookInputResource>))]
[JsonSerializable(typeof(DataWrapper<CategoryInputResourceIdentifier>))]
// Closed generic types - PaginatedDataResponse<T>
[JsonSerializable(typeof(PaginatedDataResponse<AccountResource>))]
[JsonSerializable(typeof(PaginatedDataResponse<AttachmentResource>))]
[JsonSerializable(typeof(PaginatedDataResponse<TagResource>))]
[JsonSerializable(typeof(PaginatedDataResponse<TransactionResource>))]
[JsonSerializable(typeof(PaginatedDataResponse<WebhookResource>))]
[JsonSerializable(typeof(PaginatedDataResponse<WebhookDeliveryLogResource>))]
// Closed generic types - RelatedData<T>
[JsonSerializable(typeof(RelatedData<TransactionRelatedAccount>))]
[JsonSerializable(typeof(RelatedData<CategoriesRelated>))]
[JsonSerializable(typeof(RelatedData<AttachmentRelated>))]
[JsonSerializable(typeof(RelatedData<AttachmentRelatedTransaction>))]
internal partial class UpJsonContext : JsonSerializerContext
{
}
