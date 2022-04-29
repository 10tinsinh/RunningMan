using RunningManApi.DTO.Models;
using RunningManApi.Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RunningManApi.Service
{
    public interface IJWTManagerTokenRepository
    {
        TokensDTO GenerateToken(Account account);
        TokensDTO GenerateRefreshToken(Account account);
        ClaimsPrincipal GetPrincaipalFromExpiredToken(string token);
    }
}
