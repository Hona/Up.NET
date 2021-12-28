namespace Up.NET.Models;

public class DataResponse<T> where T : class
{
    public T Data { get; set; }
    public SelfLink Links { get; set; }
}