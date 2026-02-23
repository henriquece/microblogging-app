namespace Api.Models;

public class PostComment
{
  public int Id { get; set; }

  public string Content { get; set; }

  public int UserId { get; set; }
  public User User { get; set; }

  public int PostId { get; set; }
  public Post Post { get; set; }

  public ICollection<PostCommentLike> Likes { get; set; }
}

public class PostCommentRequestDto
{
  public string Content { get; set; }

  public int UserId { get; set; }
}

public class PostCommentResponseDto
{
  public int Id { get; set; }

  public string Content { get; set; }

  public int UserId { get; set; }

  public string UserName { get; set; }

  public List<PostCommentLikeResponseDto> Likes { get; set; }
}

public class PostCommentDeleteRequestDto
{
  public int Id { get; set; }
}
