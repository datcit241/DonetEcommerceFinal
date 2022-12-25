using Application.Core;
using Application.Products;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] PagingParams pagingParams)
    {
        return HandlePagedResult(await Mediator.Send(new List.Query { QueryParams = pagingParams }));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(Product product)
    {
        return HandleResult(await Mediator.Send(new Create.Command { Product = product }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditProduct(Guid id, Product product)
    {
        product.Id = id;
        return HandleResult(await Mediator.Send(new Edit.Command { Product = product }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
    }

    [HttpGet]
    [Route("[Action]")]
    public async Task<ActionResult<List<ProductStatus>>> GetStatus()
    {
        return HandleResult(await Mediator.Send(new ListStatus.Query()));
    }
}