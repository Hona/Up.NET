# Up.NET API Wrapper Verification Checklist

This document provides a comprehensive checklist to verify that the Up.NET wrapper matches the [Up Bank API documentation](https://developer.up.com.au/).

## Accounts

### Endpoints
- [ ] `GET /accounts` - `GetAccountsAsync(int? pageSize, AccountType? accountType, OwnershipType? ownershipType)`
  - [ ] Supports `page[size]` query parameter
  - [ ] Supports `filter[accountType]` query parameter (SAVER, TRANSACTIONAL, HOME_LOAN)
  - [ ] Supports `filter[ownershipType]` query parameter (INDIVIDUAL, JOINT)
  - [ ] Returns `PaginatedDataResponse<AccountResource>`
- [ ] `GET /accounts/{id}` - `GetAccountAsync(string id)`
  - [ ] Returns `DataResponse<AccountResource>`

### Models
- [ ] `AccountResource` contains: Type, Id, Attributes, Relationships, Links
- [ ] `AccountAttributes` contains: DisplayName, AccountType, OwnershipType, Balance, CreatedAt
- [ ] `AccountType` enum: Saver, Transactional, HomeLoan
- [ ] `OwnershipType` enum: Individual, Joint
- [ ] `MoneyObject` contains: CurrencyCode, Value, ValueInBaseUnits

---

## Attachments

### Endpoints
- [ ] `GET /attachments` - `GetAttachmentsAsync(int? pageSize)`
  - [ ] Returns `PaginatedDataResponse<AttachmentResource>`
- [ ] `GET /attachments/{id}` - `GetAttachmentAsync(string id)`
  - [ ] Returns `DataResponse<AttachmentResource>`

### Models
- [ ] `AttachmentResource` contains: Type, Id, Attributes, Relationships, Links
- [ ] `AttachmentAttributes` contains: CreatedAt, FileURL, FileURLExpiresAt, FileExtension, FileContentType
- [ ] `AttachmentRelationships` contains: Transaction

---

## Categories

### Endpoints
- [ ] `GET /categories` - `GetCategoriesAsync(string parentId)`
  - [ ] Supports `filter[parent]` query parameter
  - [ ] Returns `DataResponse<List<CategoriesResource>>` (non-paginated)
- [ ] `GET /categories/{id}` - `GetCategoryAsync(string id)`
  - [ ] Returns `DataResponse<CategoriesResource>`
- [ ] `PATCH /transactions/{transactionId}/relationships/category` - `CategorizeTransactionAsync(string transactionId, string categoryId)`
  - [ ] Accepts null categoryId to de-categorize
  - [ ] Returns `NoResponse` (HTTP 204)

### Models
- [ ] `CategoriesResource` contains: Type, Id, Attributes, Relationships, Links
- [ ] `CategoriesAttributes` contains: Name
- [ ] `CategoriesRelationships` contains: Parent, Children
- [ ] `CategoriesChildren.Data` property correctly maps to JSON `data` array
- [ ] `CategoryInputResourceIdentifier` contains: Type, Id

---

## Tags

### Endpoints
- [ ] `GET /tags` - `GetTagsAsync(int? pageSize)`
  - [ ] Supports `page[size]` query parameter
  - [ ] Returns `PaginatedDataResponse<TagResource>`
- [ ] `POST /transactions/{transactionId}/relationships/tags` - `AddTagsToTransactionAsync(string transactionId, params string[] tagIds)`
  - [ ] Returns `NoResponse` (HTTP 204)
- [ ] `DELETE /transactions/{transactionId}/relationships/tags` - `RemoveTagsFromTransactionAsync(string transactionId, params string[] tagIds)`
  - [ ] Returns `NoResponse` (HTTP 204)

### Models
- [ ] `TagResource` contains: Type, Id, Relationships
- [ ] `TagRelationships` contains: Transactions
- [ ] `TagInputResourceIdentifier` contains: Type, Id

---

## Transactions

### Endpoints
- [ ] `GET /transactions` - `GetTransactionsAsync(int? pageSize, TransactionStatus? status, DateTime? since, DateTime? until, string category, string tag)`
  - [ ] Supports `page[size]` query parameter
  - [ ] Supports `filter[status]` query parameter (HELD, SETTLED)
  - [ ] Supports `filter[since]` query parameter (RFC-3339 date-time)
  - [ ] Supports `filter[until]` query parameter (RFC-3339 date-time)
  - [ ] Supports `filter[category]` query parameter
  - [ ] Supports `filter[tag]` query parameter
  - [ ] Returns `PaginatedDataResponse<TransactionResource>`
- [ ] `GET /transactions/{id}` - `GetTransactionAsync(string id, ...)`
  - [ ] Returns `DataResponse<TransactionResource>`
- [ ] `GET /accounts/{accountId}/transactions` - `GetTransactionsAsync(string accountId, ...)`
  - [ ] Supports same filters as global transactions endpoint
  - [ ] Returns `PaginatedDataResponse<TransactionResource>`

### Models
- [ ] `TransactionResource` contains: Type, Id, Attributes, Relationships, Links
- [ ] `TransactionAttributes` contains:
  - [ ] Status (TransactionStatus enum)
  - [ ] RawText
  - [ ] Description
  - [ ] Message
  - [ ] IsCategorizable
  - [ ] HoldInfo
  - [ ] RoundUp
  - [ ] Cashback
  - [ ] Amount
  - [ ] ForeignAmount
  - [ ] CardPurchaseMethod
  - [ ] SettledAt
  - [ ] CreatedAt
  - [ ] TransactionType
  - [ ] Note
  - [ ] PerformingCustomer
  - [ ] DeepLinkURL
- [ ] `TransactionRelationships` contains:
  - [ ] Account
  - [ ] TransferAccount
  - [ ] Category
  - [ ] ParentCategory
  - [ ] Tags
  - [ ] Attachment
- [ ] `TransactionStatus` enum: Held, Settled
- [ ] `CardPurchaseMethod` contains: Method, CardNumberSuffix
- [ ] `CardPurchaseMethodType` enum: BarCode, Ocr, CardPin, CardDetails, CardOnFile, Ecommerce, MagneticStripe, Contactless
- [ ] `HoldInfo` contains: Amount, ForeignAmount
- [ ] `RoundUp` contains: Amount, BoostPortion
- [ ] `Cashback` contains: Description, Amount
- [ ] `NoteObject` contains: Text
- [ ] `CustomerObject` contains: DisplayName

---

## Utility Endpoints

### Endpoints
- [ ] `GET /util/ping` - `GetPingAsync()`
  - [ ] Returns `PingResponse`

### Models
- [ ] `PingResponse` contains: Meta
- [ ] `PingMeta` contains: Id, StatusEmoji

---

## Webhooks

### Endpoints
- [ ] `GET /webhooks` - `GetWebhooksAsync(int? pageSize)`
  - [ ] Supports `page[size]` query parameter
  - [ ] Returns `PaginatedDataResponse<WebhookResource>`
- [ ] `POST /webhooks` - `CreateWebhookAsync(WebhookInputResource webhook)`
  - [ ] Returns `DataResponse<WebhookResource>` with secretKey
- [ ] `GET /webhooks/{id}` - `GetWebhookAsync(string id)`
  - [ ] Returns `DataResponse<WebhookResource>`
- [ ] `DELETE /webhooks/{id}` - `DeleteWebhookAsync(string id)`
  - [ ] Returns `NoResponse` (HTTP 204)
- [ ] `POST /webhooks/{webhookId}/ping` - `PingWebhookAsync(string webhookId)`
  - [ ] Returns `DataResponse<WebhookEventResource>`
- [ ] `GET /webhooks/{webhookId}/logs` - `GetWebhookLogsAsync(string webhookId)`
  - [ ] Returns `PaginatedDataResponse<WebhookDeliveryLogResource>`

### Models
- [ ] `WebhookResource` contains: Type, Id, Attributes, Relationships, Links
- [ ] `WebhookAttributes` contains: Url, Description, SecretKey (optional), CreatedAt
- [ ] `WebhookInputResource` / `WebhookInputAttributes` contains: Url, Description
- [ ] `WebhookRelationships` contains: Logs
- [ ] `WebhookEventResource` contains: Type, Id, Attributes, Relationships
- [ ] `WebhookEventAttributes` contains: EventType, CreatedAt
- [ ] `WebhookEventType` enum: TransactionCreated, TransactionSettled, TransactionDeleted, Ping
- [ ] `WebhookEventRelationships` contains: Webhook, Transaction
- [ ] `WebhookDeliveryLogResource` contains: Type, Id, Attributes, Relationships
- [ ] `WebhookDeliveryLogAttributes` contains: Request, Response, DeliveryStatus, CreatedAt
- [ ] `WebhookDeliveryStatus` enum: Delivered, Undeliverable, BadResponseCode

---

## Common Models

- [ ] `DataResponse<T>` - Single resource response wrapper
- [ ] `PaginatedDataResponse<T>` - Paginated list response with Links (prev, next)
- [ ] `DataWrapper<T>` - Request body wrapper for POST/PATCH
- [ ] `NoResponse` - Empty response for 204 status
- [ ] `UpResponse<T>` - Response wrapper with IsSuccess and Errors
- [ ] `ErrorResponse` / `ErrorWrapper` - Error handling models
- [ ] `RelatedData<T>` - Relationship data with Links
- [ ] `RelatedLink` - Contains Related URL
- [ ] `SelfLink` - Contains Self URL
- [ ] `PaginatedLinks` - Contains Prev and Next URLs

---

## Verification Notes

When verifying each checkbox:
1. Compare the endpoint signature with the API documentation
2. Verify all query parameters are supported
3. Check that the return type matches the expected response structure
4. Validate that model properties match the JSON schema in the API docs
5. Ensure enum values match the API's expected string values (SCREAMING_SNAKE_CASE)
