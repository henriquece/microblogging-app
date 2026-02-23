namespace Api.Models;

public class PostLike
{
  public int Id { get; set; }

  public int UserId { get; set; }
  public User User { get; set; }

  public int PostId { get; set; }
  public Post Post { get; set; }
}

public class PostLikeRequestDto
{
  public int UserId { get; set; }
}

public class PostLikeResponseDto
{
  public int UserId { get; set; }

  public string UserName { get; set; }
}
