using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if  (await UserExists(registerDto.Username)) return BadRequest("Username is taken");
        
        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            UserName = registerDto.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return new UserDto
        {
            Token = tokenService.CreateToken(user),
            Username = user.UserName
        };

    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        //check username
        var user = await context.Users.FirstOrDefaultAsync(x =>
            x.UserName == loginDto.Username.ToLower());

        if (user == null) return BadRequest("Invalid username");
        
        //check password
        using var hmac = new HMACSHA512(user.PasswordSalt);//same as pwHash in db
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("invalid password");

        }

        return new UserDto
        {
            Token = tokenService.CreateToken(user),
            Username = user.UserName
        };
    }
    
    
    private async Task<bool> UserExists(string username)
    {
        return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
    } 
    
}