using Up.NET.Models;

namespace Up.NET.Api.Categories;

public class CategoriesChildren
{
    public List<CategoriesRelated> Data { get; set; }
    public RelatedLink Links { get; set; }
}