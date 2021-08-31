using System.Collections.Generic;
using Up.NET.Api.Categories;
using Up.NET.Api.Tags;
using Up.NET.Models;

namespace Up.NET.Api.Transactions
{
    public class TransactionRelationships
    {
        public RelatedData<TransactionRelatedAccount> Account { get; set; }
        public RelatedData<CategoriesRelated> Category { get; set; }
        public RelatedData<CategoriesRelated> ParentCategory { get; set; }
        public DataResponse<List<TagInputResourceIdentifier>> Tags { get; set; }
    }
}