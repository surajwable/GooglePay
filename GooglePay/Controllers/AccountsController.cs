using AutoMapper;
using GooglePay.Data;
using GooglePay.DTOs;
using GooglePay.Models;
using GooglePay.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace GooglePay.Controllers
{
    [ApiController]
    public class AccountsController : Controller
    {
        private readonly DataContext dataContext;
        private readonly IMapper mapper;
        private readonly ITokenService tokenService;

        public AccountsController(DataContext dataContext,IMapper mapper, ITokenService tokenService)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
            this.tokenService = tokenService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.AccountHolderName)) return BadRequest("Username already exists");

            var user = mapper.Map<AccountHolder>(registerDto);

            user.AccountHolderName = registerDto.AccountHolderName;
            var hmac = new HMACSHA512();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
            user.PasswordSalt = hmac.Key;

            await dataContext.AccountHolders.AddAsync(user);
            dataContext.SaveChanges();
            return new UserDto
            {
                AccountHolderId = user.AccountHolderId,
                AccountHolderName = user.AccountHolderName,
                Token = tokenService.CreateToken(user)
            };        
        }

        private async Task<bool> UserExists(string accountHolderName)
        {
            return await dataContext.AccountHolders.AnyAsync(x => x.AccountHolderName == accountHolderName);          
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<UserDto>> Login(RegisterDto registerDto)
        {
            var user = await dataContext.AccountHolders.SingleOrDefaultAsync(x => x.AccountHolderName == registerDto.AccountHolderName);
            if (user == null) return Unauthorized("username not found, please register first");
            var hmac = new HMACSHA512(user.PasswordSalt);
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));

            for (int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != user.PasswordHash[i]) return Unauthorized("invalid password");
            }

            return new UserDto
            {
                AccountHolderId = user.AccountHolderId,
                AccountHolderName = user.AccountHolderName,
                Token = tokenService.CreateToken(user)
            };
        }


    }
}
