using Api.Data;
using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(AppDbContext appDbContext, IMapper mapper, ILogger<UsersController> logger) : ControllerBase {
  private readonly AppDbContext _appDbContext = appDbContext;

  private readonly IMapper _mapper = mapper;

  private readonly ILogger _logger = logger;

  /// <summary>
  /// List all users.
  /// </summary>
  [HttpGet]
  public List<UserDTO> GetUsers() {
    var users = _appDbContext.Users.ToList();

    var usersDTOs = _mapper.Map<List<UserDTO>>(users);

    _logger.LogInformation("Oiii7");

    return usersDTOs;
  }

  [HttpPost]
  public async Task<IActionResult> AddUser(User user) {
    _appDbContext.Users.Add(new User { Name = user.Name });

    await _appDbContext.SaveChangesAsync();

    return Ok(user);
  }
}
