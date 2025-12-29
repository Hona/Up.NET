using Up.NET.Api.Webhooks;
using Up.NET.Api.Webhooks.Events;
using Up.NET.Api.Webhooks.Logs;
using Up.NET.Models;

namespace Up.NET.Api;

public partial class UpApi
{
    public async Task<UpResponse<PaginatedDataResponse<WebhookResource>>> GetWebhooksAsync(int? pageSize = null)
    {
        var queryParams = new Dictionary<string, string>();

        if (pageSize.HasValue)
        {
            queryParams.Add("page[size]", pageSize.ToString());
        }

        return await SendPaginatedRequestAsync<WebhookResource>(HttpMethod.Get, "/webhooks", queryParams);
    }

    public async Task<UpResponse<DataResponse<WebhookResource>>> CreateWebhookAsync(WebhookInputResource webhook)
    {
        var content = new DataWrapper<WebhookInputResource>
        {
            Data = webhook
        };

        return await SendRequestAsync<DataResponse<WebhookResource>>(HttpMethod.Post, "/webhooks", content: content);
    }

    public async Task<UpResponse<DataResponse<WebhookResource>>> GetWebhookAsync(string id)
        => await SendRequestAsync<DataResponse<WebhookResource>>(HttpMethod.Get, $"/webhooks/{id}");

    public async Task<UpResponse<NoResponse>> DeleteWebhookAsync(string id)
        => await SendRequestAsync<NoResponse>(HttpMethod.Delete, $"/webhooks/{id}");

    public async Task<UpResponse<DataResponse<WebhookEventResource>>> PingWebhookAsync(string webhookId)
        => await SendRequestAsync<DataResponse<WebhookEventResource>>(HttpMethod.Post, $"/webhooks/{webhookId}/ping");

    public async Task<UpResponse<PaginatedDataResponse<WebhookDeliveryLogResource>>> GetWebhookLogsAsync(
        string webhookId)
        => await SendPaginatedRequestAsync<WebhookDeliveryLogResource>(HttpMethod.Get, $"/webhooks/{webhookId}/logs");
}