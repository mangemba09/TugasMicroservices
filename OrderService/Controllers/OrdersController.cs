using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderServices.Data;
using OrderServices.Dtos;
using OrderServices.Models;

namespace OrderServices.Controllers
{
    [Route("api/o/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepo orderRepo,IMapper mapper)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task <ActionResult<IEnumerable<ReadOrderDto>>> GetOrderAll()
        {
            var orders = await _orderRepo.GetOrderAll();
            var listOrders = _mapper.Map<IEnumerable<ReadOrderDto>>(orders);
            return Ok(listOrders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderRepo.GetOrderById(id);
            var readOrderDto = _mapper.Map<ReadOrderDto>(order);
            return Ok(readOrderDto);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(CreateOrderDto createOrderDto)
        {
            var order = _mapper.Map<Order>(createOrderDto);
            await _orderRepo.CreateOrder(order);
            _orderRepo.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = order.OrderId }, order);
        }
    }
}
