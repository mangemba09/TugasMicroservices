using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductService.Models;

namespace ProductService.Data
{
    public class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateAsyncScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if (!context.Products.Any())
            {
                Console.WriteLine("--> Seeding Data.. <--");
                context.Products.AddRange(
                    new Product()
                    {
                        Name = "Baju",
                        Stock = 20,
                        Description = "T-shirt",
                        Price = 30000
                    },
                    new Product()
                    {
                        Name = "Teh kotak",
                        Stock = 50,
                        Description = "Teh manis instan",
                        Price = 5000
                    },
                    new Product()
                    {
                        Name = "Asus",
                        Stock = 84,
                        Description = "Laptop",
                        Price = 12000000
                    });
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> Sudah ada data <--");
            }
        }
    }
}