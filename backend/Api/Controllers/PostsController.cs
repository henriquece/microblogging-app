using Api.Data;
using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController(AppDbContext appDbContext, IMapper mapper, ILogger<PostsController> logger) : ControllerBase {
  private readonly AppDbContext _appDbContext = appDbContext;

  private readonly IMapper _mapper = mapper;

  private readonly ILogger _logger = logger;

  /// <summary>
  /// List all posts.
  /// </summary>
  [HttpGet]
  [Authorize]
  public List<PostResponseDto> GetPosts() {
    var posts = _appDbContext.Posts.Select(post => new PostResponseDto {
      Id = post.Id,
      Content = post.Content,
      UserId = post.UserId,
      UserName = post.User.Name,
      Likes = post.Likes.Select(like => new PostLikeResponseDto {
        UserId = like.UserId,
        UserName = like.User.Name
      }).ToList(),
      Comments = post.Comments.Select(comment => new PostCommentResponseDto {
        Id = comment.Id,
        Content = comment.Content,
        UserId = comment.UserId,
        UserName = comment.User.Name,
        Likes = comment.Likes.Select(like => new PostCommentLikeResponseDto {
          Id = like.Id,
          UserId = like.UserId,
        }).ToList()
      }).ToList()
    }).ToList();

    return posts;
  }

  [HttpPost]
  public async Task<IActionResult> CreatePost(CreatePostDto post) {
    _appDbContext.Posts.Add(new Post {
      UserId = post.UserId,
      Content = post.Content,
    });

    await _appDbContext.SaveChangesAsync();

    return Ok(post);
  }

  [HttpDelete("{postId}")]
  public IActionResult DeletePost(int postId) {
    var post = _appDbContext.Posts
      .FirstOrDefault(post => post.Id == postId);

    _appDbContext.Posts.Remove(post);
    _appDbContext.SaveChanges();

    return NoContent();
  }

  [HttpPost("{postId}/like")]
  public async Task<IActionResult> AddPostLike(int postId, [FromBody] PostLikeRequestDto postLikeRequest) {
    _appDbContext.PostLikes.Add(new PostLike {
      UserId = postLikeRequest.UserId,
      PostId = postId,
    });

    await _appDbContext.SaveChangesAsync();

    return Ok();
  }

  [HttpDelete("{postId}/like")]
  public IActionResult RemovePostLike(int postId, [FromBody] PostLikeRequestDto postLikeRequest) {
    var like = _appDbContext.PostLikes
        .FirstOrDefault(l => l.PostId == postId && l.UserId == postLikeRequest.UserId);

    if (like == null) {
      return NotFound("Like record not found.");
    }

    _appDbContext.PostLikes.Remove(like);
    _appDbContext.SaveChanges();

    return NoContent();
  }

  [HttpPost("{postId}/comment")]
  public async Task<IActionResult> AddPostComment(int postId, [FromBody] PostCommentRequestDto postCommentRequest) {
    _appDbContext.PostComments.Add(new PostComment {
      Content = postCommentRequest.Content,
      UserId = postCommentRequest.UserId,
      PostId = postId,
    });

    await _appDbContext.SaveChangesAsync();

    return Ok();
  }

  [HttpDelete("{postId}/comment")]
  public IActionResult DeletePostComment(int postId, [FromBody] PostCommentDeleteRequestDto postLikeRequest) {
    var comment = _appDbContext.PostComments
        .FirstOrDefault(comment => comment.Id == postLikeRequest.Id);

    _appDbContext.PostComments.Remove(comment);
    _appDbContext.SaveChanges();

    return NoContent();
  }

  [HttpPost("{postId}/comment/{commentId}/like")]
  public async Task<IActionResult> AddPostCommentLike(int postId, int commentId, [FromBody] PostCommentLikeRequestDto postCommentLikeRequest) {
    _appDbContext.PostCommentLikes.Add(new PostCommentLike {
      UserId = postCommentLikeRequest.UserId,
      PostCommentId = commentId,
    });

    await _appDbContext.SaveChangesAsync();

    return Ok();
  }

  [HttpDelete("{postId}/comment/{commentId}/like")]
  public IActionResult RemovePostCommentLike(int postId, int commentId, [FromBody] PostCommentLikeDeleteRequestDto request) {
    var commentLike = _appDbContext.PostCommentLikes
        .FirstOrDefault(like => like.Id == request.Id);

    _appDbContext.PostCommentLikes.Remove(commentLike);
    _appDbContext.SaveChanges();

    return NoContent();
  }
}
