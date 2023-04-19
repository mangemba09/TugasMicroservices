using Microsoft.EntityFrameworkCore;
using WalletService.Models;

namespace WalletServices.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Wallet> Wallets { get; set; }
    }
}
