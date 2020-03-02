using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public static class CategoryExtensions
    {
        public static IList<CategoryDto> BuildTrees(this IList<Category> categories)
        {
            var dtos = categories.Select(c => new CategoryDto
            {
                Id = c.CategoryID,
                Title = c.CategoryName,
                ParentId = c.ParentCategoryID,
            }).ToList();

            return BuildTrees(null, dtos);
        }

        private static IList<CategoryDto> BuildTrees(int? pid, IList<CategoryDto> candicates)
        {
            var subs = candicates.Where(c => c.ParentId == pid).ToList();
            if (subs.Count() == 0)
            {
                return new List<CategoryDto>();  // required an empty list instead of a null ! 
            }
            foreach (var i in subs)
            {
                i.children = BuildTrees(i.Id, candicates);
            }
            return subs;
        }
    }
}
