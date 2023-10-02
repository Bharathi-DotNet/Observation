﻿using Enquiry.Web.Dtos;
using Enquiry.Web.Keys;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Enquiry.Web.Services
{
    public class TokenService
    {
        private readonly UserRepository userRepository;
        private readonly SigningAudienceCertificate signingAudienceCertificate;

        public TokenService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
            signingAudienceCertificate = new SigningAudienceCertificate();
        }

        public string GetToken(string username)
        {
            User user = userRepository.GetUser(username).GetAwaiter().GetResult();
            SecurityTokenDescriptor tokenDescriptor = GetTokenDescriptor(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(securityToken);

            return token;
        }

        private SecurityTokenDescriptor GetTokenDescriptor(User user)
        {
            const int expiringDays = 7;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(user.Claims()),
                Expires = DateTime.UtcNow.AddDays(expiringDays),
                SigningCredentials = signingAudienceCertificate.GetAudienceSigningKey()
            };

            return tokenDescriptor;
        }
    }
}
