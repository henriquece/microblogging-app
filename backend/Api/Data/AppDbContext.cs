using Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class AppDbContext : IdentityDbContext<IdentityUser> {
  public AppDbContext(DbContextOptions<AppDbContext> options)
      : base(options) {
  }

  public DbSet<Post> Posts { get; set; }

  public DbSet<PostLike> PostLikes { get; set; }

  public DbSet<PostComment> PostComments { get; set; }

  public DbSet<PostCommentLike> PostCommentLikes { get; set; }
}
