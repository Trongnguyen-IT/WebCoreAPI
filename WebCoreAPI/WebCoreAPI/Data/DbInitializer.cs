using System;
using System.Linq;

namespace WebCoreAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(WebDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any Products.
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }

            var products = new Product[]
            {
            new Product{Name="Carson",Price= 1000000,DateCreated=DateTime.Parse("2005-09-01")},
            new Product{Name="Meredith",Price= 2000000,DateCreated=DateTime.Parse("2002-09-01")},
            new Product{Name="Arturo",Price= 1000000,DateCreated=DateTime.Parse("2003-09-01")},
            new Product{Name="Gytis",Price= 1000000,DateCreated=DateTime.Parse("2002-09-01")},
            new Product{Name="Yan",Price= 1000000,DateCreated=DateTime.Parse("2002-09-01")},
            new Product{Name="Peggy",Price= 1000000,DateCreated=DateTime.Parse("2001-09-01")}
            };

            foreach (Product s in products)
            {
                context.Products.Add(s);
            }
            context.SaveChanges();

            var categorys = new Category[]
            {
            new Category{Name="Chemistry"},
            new Category{Name="Microeconomics"},
            new Category{Name="Macroeconomics"},
            new Category{Name="Calculus"},
            new Category{Name="Trigonometry"},
            new Category{Name="Composition"},
            new Category{Name="Literature"}
            };
            foreach (Category c in categorys)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();
        }
    }
}
