using AutoMapper;
using DecorGearApplication.DataTransferObj.Order;
using DecorGearDomain.Data.Entities;

namespace DecorGearInfrastructure.Extention.AutoMapperProfile
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDto, Order>().ReverseMap();
            CreateMap<CreateOrderRequest, Order>();
        }
    }
}
