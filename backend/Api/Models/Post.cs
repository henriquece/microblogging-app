namespace Api.Models;

public class Post
{
  public int Id { get; set; }

  public string Content { get; set; }

  public int UserId { get; set; }
  public User User { get; set; }

  public ICollection<PostLike> Likes { get; set; }

  public ICollection<PostComment> Comments { get; set; }
}

public class CreatePostDto
{
  public string Content { get; set; }

  public int UserId { get; set; }
}

public class PostResponseDto
{
  public int Id { get; set; }

  public string Content { get; set; }

  public int UserId { get; set; }

  public string UserName { get; set; }

  public List<PostLikeResponseDto> Likes { get; set; }

  public List<PostCommentResponseDto> Comments { get; set; }
}
