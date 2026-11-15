using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Trueka.Entities;
using Trueka.Services.DTOs;
using Trueka.Services.Helpers;
using Trueka.Services.Interfaces;

namespace Trueka.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly JwtSettings _jwt;
    public AuthService(UserManager<AppUser> userManager, IOptions<JwtSettings> jwtOptions)
    {
        _userManager = userManager;
        _jwt = jwtOptions.Value;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        var user = new AppUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            FullName = dto.FullName
        };
        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(
                string.Join(", ", result.Errors.Select(e => e.Description)));
        }
        return await BuildResponseAsync(user);
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
        {
            throw new UnauthorizedAccessException("Correo electrónico o contraseña incorrectos");
        }
        return await BuildResponseAsync(user);
    }

    private async Task<AuthResponseDto> BuildResponseAsync(AppUser user)
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(_jwt.ExpiresMinutes);
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
    {
        new(ClaimTypes.NameIdentifier, user.Id),
        new(ClaimTypes.Email, user.Email!),
        new(ClaimTypes.Name, user.FullName)
    };
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);

        return new AuthResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresAt = expiresAt,
            UserId = user.Id,
            FullName = user.FullName,
            Roles = roles
        };
    }

}
