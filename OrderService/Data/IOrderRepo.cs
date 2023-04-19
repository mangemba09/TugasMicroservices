using OrderServices.Models;
using System.Threading.Tasks;

namespace OrderServices.Data
{
    public interface IOrderRepo
    {
        Task<Order> CreateOrder(Order order);
        Task<IEnumerable<Order>> GetOrderAll();
        Task<Order> GetOrderById(int orderId);
        bool SaveChanges();
    }
}
