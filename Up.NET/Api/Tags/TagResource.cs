namespace Up.NET.Api.Tags;

public class TagResource
{
    [Obsolete("Always `tags`")]
    public string Type { get; set; }

    public string Id { get; set; }
}