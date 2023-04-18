using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletService.Dtos
{
    public class ReadWalletDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Cash { get; set; }
    }
}