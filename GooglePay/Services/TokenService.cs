using GooglePay.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GooglePay.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            //this key should be registered inside config file means in appsetting.development.json
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(AccountHolder accountHolder)
        {
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, accountHolder.AccountHolderId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Name, accountHolder.AccountHolderName),
                    new Claim(JwtRegisteredClaimNames.UniqueName, accountHolder.AccountHolderName)
                };

            //creating signing credentials to sign this token with.
            //creating credentials which takes arguments like 1.security key ,2.algorithms we want to use, 
            var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //describing the token which tells about what our token includes. we are going to return 

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = cred
            };

            //creating token handler
            var tokenHandler = new JwtSecurityTokenHandler();

            //finally creating a token 

            var token = tokenHandler.CreateToken(tokenDescriptor);

            // WriteToken() : converts JWT object into its string format, allowing it to be transmitted and used for authentication and authorization

            return tokenHandler.WriteToken(token);
        }
    }
}
