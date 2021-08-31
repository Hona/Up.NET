using System.Net.Http;
using System.Threading.Tasks;
using Up.NET.Api.Utilities;
using Up.NET.Models;

namespace Up.NET.Api
{
    public partial class UpApi
    {
        public async Task<UpResponse<PingResponse>> GetPingAsync() 
            => await SendRequestAsync<PingResponse>(HttpMethod.Get, "/util/ping");
    }
}