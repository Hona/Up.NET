using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Up.NET.Models
{
    public class PaginatedDataResponse<T> where T : class
    {
        public List<T> Data { get; set; }

        public PaginatedLinks Links { get; set; }
    }
}