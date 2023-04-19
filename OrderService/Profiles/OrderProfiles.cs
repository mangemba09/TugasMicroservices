using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OrderServices.Dtos;
using OrderServices.Models;

namespace OrderService.Profiles
{
    public class OrderProfiles : Profile
    {
         public OrderProfiles()
        {
            CreateMap<CreateOrderDto, Order>();
            CreateMap<Order, ReadOrderDto>();
            CreateMap<Order, ReadAllOrder>();
        }
    }
}