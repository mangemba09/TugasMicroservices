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
                        Username = "user1",
                        Fullname = "Lucy Mangemba",
                        Cash = 100000
                    },
                    new Wallet()
                    {
                        Username = "user2",
                        Fullname = "Try Lois",
                        Cash = 200000
                    },
                    new Wallet()
                    {
                        Username = "user3",
                        Fullname = "Lidya Datu Langi",
                        Cash = 300000
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
