using AutoMapper;
using MyToolbox.Shared.Models;
using MyToolbox.Shared.Models.Requests;
using Entities = MyToolbox.DataAccessLayer.Entities;

namespace MyToolbox.BusinessLayer.MapperProfiles;

public class OrderMapperProfile : Profile
{
    public OrderMapperProfile()
    {
        CreateMap<Entities.Order, Order>();

        CreateMap<SaveOrderRequest, Entities.Order>();
    }
}