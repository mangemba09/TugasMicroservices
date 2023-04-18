using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductService.Models;

namespace ProductService.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public void CreateProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            _context.Products.Add(product);
        }

        public async Task DeleteProduct(int id)
        {

            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
                if (product == null)
                {
                    throw new Exception("product not found");
                }
                _context.Products.Remove(product); // Hapus data dari list
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating product: {ex.Message}");
            }
        }

        public IEnumerable<Product> GetAllProduct()
        {
            return _context.Products.ToList();
        }

        public async Task<Product> GetById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
            {
                throw new Exception("product not found");
            }
            return product;
            //throw new NotImplementedException();
        }

        public async Task<Product> GetByName(string name)
        {
            var nameProduct = name.ToLower();
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Name.ToLower() == name);
            if (product == null)
            {
                throw new Exception("Product Name is not found");
            }
            return product;
        }

        public async Task Update(int id, Product product)
        {
            try
            {
                var existingProduct = await GetById(product.ProductId);
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating product: {ex.Message}");
            }
            //throw new NotImplementedException();
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
 
    }
}