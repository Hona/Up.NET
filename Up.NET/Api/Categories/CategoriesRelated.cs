using System;

namespace Up.NET.Api.Categories
{
    public class CategoriesRelated
    {
        [Obsolete("Always `categories`")]
        public string Type { get; set; }

        public string Id { get; set; }
    }
}