using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnePlan.Business.Dto.Auth;
using OnePlan.Business.Enums;
using OnePlan.Business.Exceptions;
using OnePlan.Business.Models;
using OnePlan.Business.Settings;
using OnePlan.Business.ViewModels;
using OnePlan.Core.Services.Interfaces;
using OnePlan.Data.Context;
using Microsoft.IdentityModel.Tokens;
using OnePlan.Business.Consts;
using System.Text;

namespace OnePlan.Core.Services.Implemntation;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly AppDbContext _context;
    
    private readonly JwtSettings _jwtSettings;

    public AuthService(UserManager<User> userManager,
        AppDbContext context,
        IOptions<JwtSettings> jwtOptions)
    {
        _userManager = userManager;
        _context = context;
        _jwtSettings = jwtOptions.Value;
    }

    public async Task<LoginViewModel> Login(LoginDto loginDto)
    {
        User dbUser = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u =>
                (u.UserName == loginDto.Login || u.Email == loginDto.Login) &&
                u.Status == UserStatus.Default);
        if (dbUser != null)
        {
            bool isPasswordCorrect = await _userManager.CheckPasswordAsync(dbUser, loginDto.Password);

            if (isPasswordCorrect)
                return await GenerateJwtToken(dbUser);
        }
        throw new NotAuthentificatedException("Login failed!");
    }

    private async Task<LoginViewModel> GenerateJwtToken(User user, int? expireSeconds = null)
    {
        var unixStartDate = new DateTime(1970, 1, 1);
        
        IList<string> userRoles = await _userManager.GetRolesAsync(user);

        long notValidBeforeTime = (long)DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(5))
            .Subtract(unixStartDate).TotalSeconds;

        long expirationTime = (long)DateTime.UtcNow.AddSeconds(expireSeconds ?? _jwtSettings.ExpireSeconds)
            .Subtract(unixStartDate).TotalSeconds;

        var payload = new JwtPayload
        {
            {
                JwtRegisteredClaimNames.Sub,
                user.UserName
            },
            {
                JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString()
            },
            {
                JwtRegisteredClaimNames.Nbf,
                notValidBeforeTime
            },
            {
                JwtRegisteredClaimNames.Exp,
                expirationTime
            },
            {
                JwtRegisteredClaimNames.Iss,
                _jwtSettings.Issuer
            },
            {
                JwtRegisteredClaimNames.Aud,
                _jwtSettings.Audience
            },
            {
                CustomClaims.UserId,
                user.Id.ToString()
            },
            {
                CustomClaims.Email,
                user.Email
            },
            {
                CustomClaims.Role,
                userRoles[0]
            }
        };

        byte[] jwtKey = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
        SymmetricSecurityKey key = new(jwtKey);
        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);
        JwtHeader header = new(credentials);
        JwtSecurityToken securityToken = new(header, payload);
        JwtSecurityTokenHandler handler = new();
        string token = handler.WriteToken(securityToken);
        return new LoginViewModel
        {
            JwtToken = token
        };
    }
}
