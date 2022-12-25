using Application.Carts;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CartsController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> ListCartDetails()
    {
        return HandleResult(await Mediator.Send(new List.Query()));
    }

    [HttpGet("[Action]")]
    public async Task<IActionResult> GetCartDetails(CartDetails cartDetails)
    {
        return HandleResult(await Mediator.Send(new Details.Query { CartDetails = cartDetails }));
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(CartDetails cartDetails)
    {
        return HandleResult(await Mediator.Send(new AddToCart.Command { CartDetails = cartDetails }));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteFromCart(CartDetails cartDetails)
    {
        return HandleResult(await Mediator.Send(new DeleteFromCarts.Command { CartDetails = cartDetails }));
    }
}