namespace Up.NET.Models;

public class RelatedData<T> where T : class
{
    public T Data { get; set; }
    public RelatedLink Links { get; set; }
}