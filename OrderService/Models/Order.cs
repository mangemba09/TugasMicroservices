using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrderServices.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; } 
        public int Quantity { get; set; } 
        public int Price { get; set; } 
        public DateTime OrderDate { get; set; }  

        [ForeignKey("Wallet")]
        public string Username { get; set; }
        public virtual Wallet Wallet { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
