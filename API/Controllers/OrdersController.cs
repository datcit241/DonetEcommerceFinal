using Application.Core;
using Application.Orders;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class OrdersController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetOrders([FromQuery] PagingParams pagingParams)
    {
        return HandlePagedResult(await Mediator.Send(new List.Query { QueryParams = pagingParams }));
    }

    [HttpGet("[Action]")]
    public async Task<IActionResult> GetOrderHistory([FromQuery] PagingParams pagingParams)
    {
        return HandlePagedResult(await Mediator.Send(new History.Query { QueryParams = pagingParams }));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(Coupon? coupon)
    {
        return HandleResult(await Mediator.Send(new Create.Command { Coupon = coupon }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditProduct(Guid id, CustomerOrder customerOrder)
    {
        customerOrder.Id = id;
        return HandleResult(await Mediator.Send(new Edit.Command { CustomerOrder = customerOrder }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
    }
}