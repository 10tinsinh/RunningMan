using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RunningManApi.DTO.Models;
using RunningManApi.Repository.Entites;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RunningManApi.Service
{
    public class JWTManagerTokenRepository : IJWTManagerTokenRepository
    {
        private readonly AppSetting _appSetting;
        private readonly IConfiguration configuration;

        public JWTManagerTokenRepository(IOptionsMonitor<AppSetting> optionsMonitor, IConfiguration configuration )
        {
            
            _appSetting = optionsMonitor.CurrentValue;
            this.configuration = configuration;
        }
        public TokensDTO GenerateRefreshToken(Account account)
        {
            return GenerateJWTToken(account);
        }

        public TokensDTO GenerateToken(Account account)
        {
            return GenerateJWTToken(account);
        }

        public ClaimsPrincipal GetPrincaipalFromExpiredToken(string token)
        {
            var Key = Encoding.UTF8.GetBytes(configuration["AppSettings:SecretKey"]);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }


            return principal;
        }

        public TokensDTO GenerateJWTToken(Account account)
        {
            var jwtToken = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSetting.SecretKey);
            var jwtTokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim (ClaimTypes.Name, account.Name),
                    new Claim (ClaimTypes.Email, account.Email),
                    new Claim ("Username", account.UserName),
                    new Claim ("Id", account.Id.ToString()),
                    new Claim ("AccountStatus", account.AccountStatus.ToString()),

                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)

            };
            var token = jwtToken.CreateToken(jwtTokenDescription);
            var refreshToken = GenerateRefreshToken();
            return new TokensDTO
            {
                AccessToken = jwtToken.WriteToken(token),
                RefreshToken = refreshToken
            };
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
