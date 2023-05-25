using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController: BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context,ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }
        [HttpPost("register")] //this is a POST method: api/account/register.
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(await UserExists(registerDto.Username)) return BadRequest("name already exists");
            //if any class uses Idisposable method then they have to use dispose(). so we are using "using".
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName =  registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key

            };
            //this will add the user and the next step will save the added users.
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Username = user.UserName,
                //Token =_tokenService.CreateToken(user)
            };
        }

        [HttpPost("Login")]

        public async Task<ActionResult<UserDto>>Login(LoginDto loginDto)
        {
            //singleordefault will return only element of the input or else it will retwun default value if empty.
            var user = await _context.Users.SingleOrDefaultAsync(x=>
            x.UserName == loginDto.UserName);

            if(user==null) return Unauthorized();

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for(int i=0;i<computedHash.Length; i++)
            {
                if(computedHash[i] !=user.PasswordHash[i]) return Unauthorized("invalid user");
            }
            return new UserDto
            {
                Username = user.UserName,
                //Token =   _tokenService.CreateToken(user)
            };
        }
        //to check if username already exists in table.
        private async Task<bool> UserExists(string username)
        {
            //islower is used to convert user entered name to lower. we have to take input in lowercase aswell.
           return await _context.Users.AnyAsync(x=>x.UserName==username.ToLower());
        }
    }
}