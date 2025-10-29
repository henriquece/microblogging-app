using Microsoft.EntityFrameworkCore;

namespace Api.Models;

[Index(nameof(Name), IsUnique = true)]
public class Product {
  public int Id { get; set; }

  public string Name { get; set; }
}

public class ProductDTO {
  public string Name { get; set; }
}
