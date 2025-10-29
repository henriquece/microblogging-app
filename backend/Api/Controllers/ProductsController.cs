using Api.Data;
using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController(AppDbContext appDbContext, IMapper mapper, ILogger<ProductsController> logger) : ControllerBase {
  private readonly AppDbContext _appDbContext = appDbContext;

  private readonly IMapper _mapper = mapper;

  private readonly ILogger _logger = logger;

  /// <summary>
  /// List all products.
  /// </summary>
  [HttpGet]
  public List<ProductDTO> GetProducts() {
    var products = _appDbContext.Products.ToList();

    var productsDTOs = _mapper.Map<List<ProductDTO>>(products);

    _logger.LogInformation("Oiii");

    return productsDTOs;
  }

  [HttpPost]
  public async Task<IActionResult> AddProduct(Product product) {
    _appDbContext.Products.Add(new Product { Name = product.Name });

    await _appDbContext.SaveChangesAsync();

    return Ok(product);
  }
}
