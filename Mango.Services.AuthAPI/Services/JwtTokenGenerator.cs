﻿using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Services.IService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mango.Services.AuthAPI.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions options;

        public JwtTokenGenerator(IOptions<JwtOptions> options)
        {
            this.options = options.Value;
        }
        public string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(options.Secret);
            var claimList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email , applicationUser.Email),
                new Claim(JwtRegisteredClaimNames.Sub , applicationUser.Id),
                new Claim(JwtRegisteredClaimNames.Name , applicationUser.Name),
            };

            claimList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = options.Audience,
                Issuer = options.Issues,
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token =  tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
