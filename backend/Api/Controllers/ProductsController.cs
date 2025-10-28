using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController(AppDbContext appDbContext) : ControllerBase {
  private readonly AppDbContext _appDbContext = appDbContext;

  /// <summary>
  /// List all products.
  /// </summary>
  [HttpGet]
  public List<string> GetProducts() {
    var products = _appDbContext.Products.Select(product => product.Name).ToList();

    return products;
  }

  [HttpPost]
  public async Task<IActionResult> AddProduct(Product product) {
    _appDbContext.Products.Add(new Product { Name = product.Name });

    await _appDbContext.SaveChangesAsync();

    return Ok(product);
  }
}
