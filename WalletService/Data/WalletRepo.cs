using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WalletService.Models;

namespace WalletService.Data
{
    public class WalletRepo : IWalletRepo
    {
        private readonly AppDbContext _context;
        public WalletRepo(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Wallet> GetAllWallet()
        {
            return _context.Wallets.ToList();
        }

        public async Task<Wallet> GetByName(string name)
        {
            var nameWallet = name.ToLower();
            var wallet = await _context.Wallets.FirstOrDefaultAsync(p => p.UserName.ToLower() == name);
            if (wallet == null)
            {
                throw new Exception("Wallet Name is not found");
            }
            return wallet;
        }

        public async Task TopupWallet(Wallet wallet)
        {
            try
            {
                var existingWallet = await GetByName(wallet.UserName);
                if (existingWallet == null)
                {
                    throw new Exception("Wallet Name is not found");
                }
                int topUp = existingWallet.Cash + wallet.Cash;
                existingWallet.Cash = topUp;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating product: {ex.Message}");
            }
            //throw new NotImplementedException();
        }

        public async Task OrderWallet(Wallet wallet)
        {
            try
            {
                var existingWallet = await GetByName(wallet.UserName);
                if (existingWallet == null)
                {
                    throw new Exception("Wallet Name is not found");
                }
                int cashOut = existingWallet.Cash - wallet.Cash;
                existingWallet.Cash = cashOut;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating product: {ex.Message}");
            }
            //throw new NotImplementedException();
        }

        // public async Task OrderWallet(string name, Wallet wallet)
        // {
        //     try
        //     {
        //         var existingWallet = await GetByName(name);
        //         if (existingWallet == null)
        //         {
        //             throw new Exception("Wallet Name is not found");
        //         }
        //         int topUp = existingWallet.Cash - wallet.Cash;
        //         existingWallet.Cash = topUp;
        //     }
        //     catch (Exception ex)
        //     {
        //         throw new Exception($"Error updating product: {ex.Message}");
        //     }
        //     //throw new NotImplementedException();
        // }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}