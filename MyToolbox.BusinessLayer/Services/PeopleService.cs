using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MyToolbox.BusinessLayer.Services.Interfaces;
using MyToolbox.DataAccessLayer;
using MyToolbox.Shared;
using Entities = MyToolbox.DataAccessLayer.Entities;

namespace MyToolbox.BusinessLayer.Services;

public class PeopleService : IPeopleService
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;

    public PeopleService(IDataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<Person>> GetListAsync(string name)
    {
        var query = dataContext.GetData<Entities.Person>();

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(p => p.Name.Contains(name));
        }

        var people = await query.OrderBy(p => p.Name)
            .ProjectTo<Person>(mapper.ConfigurationProvider)
            .ToListAsync();

        //var people = mapper.Map<IEnumerable<Person>>(dbPeople);

        return people;
    }
}
