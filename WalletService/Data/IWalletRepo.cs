using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletService.Models;

namespace WalletService.Data
{
    public interface IWalletRepo
    {
        IEnumerable<Wallet> GetAllWallet();
        // Task<Wallet> GetById(int id);
        Task<Wallet> GetByName(string name);
        Task TopupWallet(Wallet wallet);
        Task OrderWallet(Wallet wallet);
        bool SaveChanges();
    }
}