using System.Security.Claims;
using API.DTOs.User;
using API.Services;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[Controller]")]
public class AccountsController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly TokenService _tokenService;

    public AccountsController(UserManager<User> userManager, SignInManager<User> signInManager,
        TokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    [HttpPost]
    [Route("[Action]")]
    public async Task<ActionResult<UserDTO>> Login(Login login)
    {
        Console.Out.WriteLine(login);
        var user = await _userManager.FindByEmailAsync(login.Email);

        if (user == null) return Unauthorized();

        var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

        if (result.Succeeded)
        {
            return CreateUserDto(user);
        }

        return Unauthorized();
    }

    [HttpPost]
    [Route("[Action]")]
    public async Task<ActionResult<UserDTO>> Register(Register register)
    {
        if (await _userManager.Users.AnyAsync(user => user.Email == register.Email))
        {
            ModelState.AddModelError("email", "This email address already exists");
            // return BadRequest("The email address already exists");
        }

        if (await _userManager.Users.AnyAsync(user => user.UserName == register.UserName))
        {
            ModelState.AddModelError("username", "This username already exists");
            // return BadRequest("The username already exists");
        }

        if (ModelState.ErrorCount != 0)
        {
            return ValidationProblem();
        }

        var user = new User
        {
            UserName = register.UserName,
            Email = register.Email,
            Name = register.Name,
            Address = register.Address
        };

        var result = await _userManager.CreateAsync(user, register.Password);
        if (result.Succeeded)
        {
            return CreateUserDto(user);
        }

        return BadRequest("Problem registering user");
    }

    [Authorize]
    [HttpGet]
    [Route("[Action]")]
    public async Task<ActionResult<UserDTO>> GetCurrentUser()
    {
        var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
        return CreateUserDto(user);
    }

    private UserDTO CreateUserDto(User user)
    {
        return new UserDTO
        {
            Name = user.Name,
            Address = user.Address,
            UserName = user.UserName,
            Token = _tokenService.CreateToken(user),
            Email = user.Email
        };
    }
}