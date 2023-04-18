using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletService.Models;

namespace WalletService.Data
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
            if (!context.Wallets.Any())
            {
                Console.WriteLine("--> Seeding Data.. <--");
                context.Wallets.AddRange(
                    new Wallet()
                    {
                        UserName = "Lucy",
                        FullName = "Lucy Mangemba",
                        Cash = 100000
                    },
                    new Wallet()
                    {
                        UserName = "Lois",
                        FullName = "Try Lois",
                        Cash = 200000
                    },
                    new Wallet()
                    {
                        UserName = "Lidya",
                        FullName = "Lidya Datu Langi",
                        Cash = 50000
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