using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductService.Models;

namespace ProductService.Data
{
    public interface IProductRepository
    {
         Task<IEnumerable<Product>> GetAllProduct();
        Task<Product> GetById(int id);
        Task<Product> GetByName(string name);
        Task Create(Product product);
        Task Update(int id, Product product);
        Task Delete(int id);
        bool SaveChanges();

    }
}