using System.Text.Json.Serialization;

namespace Up.NET.Models
{
    public class DataWrapper<T>
    {
        public T Data { get; set; }
    }
}