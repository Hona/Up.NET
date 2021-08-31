using System.Collections.Generic;
using Up.NET.Models;

namespace Up.NET.Api.Categories
{
    public class CategoriesChildren
    {
        public List<CategoriesRelated> Children { get; set; }
        public RelatedLink Links { get; set; }
    }
}