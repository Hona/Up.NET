using Up.NET.Models;

namespace Up.NET.Api.Categories;

public class CategoriesResource
{
    [Obsolete("Always `categories`")]
    public string Type { get; set; }

    public string Id { get; set; }
    public CategoriesAttributes Attributes { get; set; }
    public CategoriesRelationships Relationships { get; set; }
    public SelfLink Links { get; set; }
}