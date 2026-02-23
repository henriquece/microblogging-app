namespace Api.Models;

public class PostCommentLike
{
  public int Id { get; set; }

  public int UserId { get; set; }
  public User User { get; set; }

  public int PostCommentId { get; set; }
  public PostComment PostComment { get; set; }
}

public class PostCommentLikeRequestDto
{
  public int UserId { get; set; }
}

public class PostCommentLikeResponseDto
{
  public int Id { get; set; }

  public int UserId { get; set; }
}

public class PostCommentLikeDeleteRequestDto
{
  public int Id { get; set; }
}
