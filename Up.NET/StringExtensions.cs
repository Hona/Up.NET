using System;
using System.Collections.Generic;
using System.Web;

namespace Up.NET
{
    internal static class StringExtensions
    {
        internal static Uri AddQueryParameter(this Uri baseUri, string name, string value)
        {
            var httpValueCollection = HttpUtility.ParseQueryString(baseUri.Query);

            httpValueCollection.Remove(name);
            httpValueCollection.Add(name, value);

            var ub = new UriBuilder(baseUri)
            {
                Query = httpValueCollection.ToString() ?? string.Empty
            };

            return ub.Uri;
        }
    }
}