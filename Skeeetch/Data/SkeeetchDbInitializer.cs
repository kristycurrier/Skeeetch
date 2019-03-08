using Skeeetch.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace Skeeetch.Data
{
    public class SkeeetchDbInitializer : CreateDatabaseIfNotExists<SkeeetchContext>
    {
        protected override void Seed(SkeeetchContext context)
        {
            var categories = new List<Category>
            {
                new Category{ID = 1, DisplayTerm = "", SearchTerm = ""},
                new Category{ID = 2, DisplayTerm = "", SearchTerm = ""},
                new Category{ID = 3, DisplayTerm = "", SearchTerm = ""},
                new Category{ID = 4, DisplayTerm = "", SearchTerm = ""},
                new Category{ID = 5, DisplayTerm = "", SearchTerm = ""}
            };

            categories.ForEach(k => context.Categories.Add(k));
            context.SaveChanges();
        }
    }
}