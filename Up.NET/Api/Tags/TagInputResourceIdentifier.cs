using System;

namespace Up.NET.Api.Tags
{
    public class TagInputResourceIdentifier
    {
        [Obsolete("Always `tags`")] public string Type { get; set; } = "tags";

        public string Id { get; set; }
    }
}