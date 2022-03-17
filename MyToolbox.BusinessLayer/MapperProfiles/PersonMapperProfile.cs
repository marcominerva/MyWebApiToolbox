using AutoMapper;
using MyToolbox.Shared.Models;
using Entities = MyToolbox.DataAccessLayer.Entities;

namespace MyToolbox.BusinessLayer.MapperProfiles;

public class PersonMapperProfile : Profile
{
    public PersonMapperProfile()
    {
        CreateMap<Entities.Person, Person>();
        //.ForMember(dst=>dst.City,opt=>opt.MapFrom(source=>source.Address.City));
    }
}