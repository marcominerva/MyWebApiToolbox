using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MyToolbox.BusinessLayer.Services.Interfaces;
using MyToolbox.DataAccessLayer;
using MyToolbox.Shared.Enums;
using MyToolbox.Shared.Models;
using MyToolbox.Shared.Models.Requests;
using Entities = MyToolbox.DataAccessLayer.Entities;

namespace MyToolbox.BusinessLayer.Services;

public class OrderService : IOrderService
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;

    public OrderService(IDataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<Order>> GetListAsync()
    {
        var query = dataContext.GetData<Entities.Order>();

        var orders = await query.OrderByDescending(o => o.CreationDate)
            .ProjectTo<Order>(mapper.ConfigurationProvider)
            .ToListAsync();

        //var people = mapper.Map<IEnumerable<Person>>(dbPeople);

        return orders;
    }

    public async Task<Order> SaveAsync(SaveOrderRequest order)
    {
        var dbOrder = order.Id != null ? await dataContext.GetData<Entities.Order>(trackingChanges: true)
            .FirstOrDefaultAsync(o => o.Id == order.Id) : null;

        if (dbOrder == null)
        {
            dbOrder = mapper.Map<Entities.Order>(order);
            dbOrder.CreationDate = DateTime.UtcNow;
            dbOrder.Status = OrderStatus.New;

            dataContext.Insert(dbOrder);
        }
        else
        {
            mapper.Map(order, dbOrder);
            dbOrder.LastModifiedDate = DateTime.UtcNow;
        }

        await dataContext.SaveAsync();

        var savedOrder = mapper.Map<Order>(dbOrder);
        return savedOrder;
    }
}
