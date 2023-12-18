using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Productos.Models;
using System.Net;

namespace Productos.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
        

    private readonly ILogger<ProductController> _logger;
    private readonly DBConnection _connection;

    public ProductController(ILogger<ProductController> logger, DBConnection connection)
    {
        _logger=logger;
        _connection=connection;
    }

    [HttpGet(Name = "Avaliable")]
    public async Task<IActionResult> GetAvaliableProducts()
    {

        _logger.Log(LogLevel.Information , "Returning avaliable products");
        List<Product> products = await _connection.Products
            .Where(p => p.ValidUntilDate > DateTime.Now.Date
            && p.Avaliability > 0
            ).ToListAsync();
        if (products.Count == 0)
        {
            _logger.Log(LogLevel.Warning, "Not avaliable products");
            return NotFound();
        }
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct (ProductRequest request)
    {
        _logger.Log(LogLevel.Information, "Creating product " + request);
        Product p = new Product(request.ProductName, request.ValidUntilDate, request.Cost, request.Avaliability);
        _connection.Add(p);
        await _connection.SaveChangesAsync();
        _logger.Log(LogLevel.Information, "Product " + p + " added succesfully");
        return Ok(p);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProduct([FromQuery] string productId)
    {
        _logger.Log(LogLevel.Information, "Product to be deleted " + productId);
        Product? p = _connection.Products.Find(productId);
        if (p == null)
        {
            _logger.Log(LogLevel.Warning, "Couldn't delete produc " + productId);
            return NotFound("factura no encontrada");
        }

        _connection.Products.Remove(p);
        await _connection.SaveChangesAsync();
        _logger.Log(LogLevel.Information, "Product deleted with id " + productId);
        return Ok(p);
    }

}
