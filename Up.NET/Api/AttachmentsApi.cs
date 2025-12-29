using Up.NET.Api.Attachments;
using Up.NET.Models;

namespace Up.NET.Api;

public partial class UpApi
{
    public async Task<UpResponse<PaginatedDataResponse<AttachmentResource>>> GetAttachmentsAsync(int? pageSize = null)
    {
        var queryParams = new Dictionary<string, string>();

        if (pageSize.HasValue)
        {
            queryParams.Add("page[size]", pageSize.ToString());
        }

        return await SendPaginatedRequestAsync<AttachmentResource>(HttpMethod.Get, "/attachments", queryParams);
    }

    public async Task<UpResponse<DataResponse<AttachmentResource>>> GetAttachmentAsync(string id)
        => await SendRequestAsync<DataResponse<AttachmentResource>>(HttpMethod.Get, $"/attachments/{id}");
}
