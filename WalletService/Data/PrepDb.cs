using WalletService.Models;

namespace WalletServices.Data
{
    public class PrepDb
    {
        private readonly WalletRepo _repo;
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateAsyncScope()) 
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if(!context.Wallets.Any()) 
            {
                Console.WriteLine("--> Seeding data <--");
                context.Wallets.AddRange(
                    new Wallet()
                    {
                        UserName = "user1",
                        FullName = "Lucy Mangemba",
                        Cash = 1000
                    },
                    new Wallet()
                    {
                        UserName = "user2",
                        FullName = "Try Lois",
                        Cash = 2000
                    },
                    new Wallet()
                    {
                        UserName = "user3",
                        FullName = "Lidya Datu Langi",
                        Cash = 3000
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
