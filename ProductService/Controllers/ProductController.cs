using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductService.Data;
using ProductService.Dtos;
using ProductService.Models;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;

        }
        [HttpGet]
        public ActionResult<IEnumerable<ReadProductDto>> GetAllProduct()
        {
            var productItem = _repo.GetAllProduct();
            var productReadDtoList = _mapper.Map<IEnumerable<ReadProductDto>>(productItem);
            return Ok(productReadDtoList);
        }

        [HttpGet("{id}", Name = "GetByProductId")]
        public async Task<ActionResult> GetByProductId(int id)
        {
            var product = await _repo.GetById(id);
            var readProduct = _mapper.Map<ReadProductDto>(product);
            return Ok(readProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, UpdateProductDto updateProductDto)
        {
            try
            {
                var product = _mapper.Map<Product>(updateProductDto);
                product.ProductId = id;
                await _repo.Update(id, product);
                _repo.SaveChanges();
                var readProductDto = _mapper.Map<ReadProductDto>(product);
                return Ok(readProductDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                await _repo.DeleteProduct(id);
                _repo.SaveChanges();
                return Ok("Data berhasil dihapus");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ReadProductDto>> CreateProduct(CreateProductDto CreateProductDto)
        {
            var productModel = _mapper.Map<Product>(CreateProductDto);
            _repo.CreateProduct(productModel);
            _repo.SaveChanges();
            var readProductDto = _mapper.Map<ReadProductDto>(productModel);
            return CreatedAtRoute(nameof(GetByProductId),
                 new { Id = readProductDto.ProductId }, readProductDto);
        }
    }
}
