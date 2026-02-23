namespace Api.Models;

public class User
{
  public int Id { get; set; }

  public string Name { get; set; }

  public ICollection<Post> Posts { get; }
}

public class UserDTO
{
  public int Id { get; set; }

  public string Name { get; set; }
}
