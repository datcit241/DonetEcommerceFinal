using Application.Products;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        return await Mediator.Send(new List.Query());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(Guid id)
    {
        return await Mediator.Send(new Details.Query { Id = id });
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(Product product)
    {
        return Ok(await Mediator.Send(new Create.Command { Product = product }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditProduct(Guid id, Product product)
    {
        product.Id = id;
        return Ok(await Mediator.Send(new Edit.Command { Product = product }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        return Ok(await Mediator.Send(new Delete.Command { Id = id }));
    }

    [HttpGet]
    [Route("[Action]")]
    public async Task<ActionResult<List<ProductLabel>>> GetLabels()
    {
        return await Mediator.Send(new ListLabels.Query());
    }

    [HttpGet]
    [Route("[Action]")]
    public async Task<ActionResult<List<ProductStatus>>> GetStatus()
    {
        return await Mediator.Send(new ListStatus.Query());
    }
}