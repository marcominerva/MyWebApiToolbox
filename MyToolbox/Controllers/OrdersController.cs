using Microsoft.AspNetCore.Mvc;
using MyToolbox.BusinessLayer.Services.Interfaces;
using MyToolbox.Shared.Models.Requests;

namespace MyToolbox.Controllers;

public class OrdersController : ControllerBase
{
    private readonly IOrderService orderService;

    public OrdersController(IOrderService orderService)
    {
        this.orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var orders = await orderService.GetListAsync();
        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> Save(SaveOrderRequest order)
    {
        var savedOrder = await orderService.SaveAsync(order);
        return Ok(savedOrder);
    }
}