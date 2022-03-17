using MyToolbox.Shared.Models;
using MyToolbox.Shared.Models.Requests;

namespace MyToolbox.BusinessLayer.Services.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetListAsync();

    Task<Order> SaveAsync(SaveOrderRequest order);
}