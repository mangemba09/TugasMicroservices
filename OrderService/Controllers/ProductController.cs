using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderServices.Data;
using OrderServices.Models;

namespace OrderServices.Controllers
{
    [Route("api/order/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _productRepo;

        public ProductController(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }
        [HttpPost("Sync")]
        public async Task<ActionResult> SyncProducts()
        {
            try
            {
                await _productRepo.CreateProduct();
                return Ok("Products Synced");
            }
            catch (Exception ex)
            {
                return BadRequest($"Could not sync product: {ex.Message}");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetPlatforms()
        {
            Console.WriteLine("--> Getting Product from OrderService");
            var productItems = await _productRepo.GetAllProducts();
            return Ok(productItems);
        }
    }
}
