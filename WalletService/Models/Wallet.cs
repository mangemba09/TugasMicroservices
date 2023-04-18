using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WalletService.Models
{
    public class Wallet
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public int Cash { get; set; }
    }
}