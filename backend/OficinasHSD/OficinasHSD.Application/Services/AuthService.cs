using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OficinasHSD.Application.DTOs;
using OficinasHSD.Application.Interfaces.Servicos;
using OficinasHSD.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace OficinasHSD.Application.Services;
public class AuthService : IAuthService
{
    private readonly IUsuarioService _usuarioService;
    private readonly IConfiguration _configuration;

    public AuthService(IUsuarioService usuarioService, IConfiguration configuration)
    {
        _usuarioService = usuarioService;
        _configuration = configuration;
    }

    public async Task<UsuarioDTO> RegistrarUsuarioAsync(UsuarioDTO dto)
    {
        
        var hashed = BCrypt.Net.BCrypt.HashPassword(dto.PasswordHash);
        var user = new UsuarioDTO
        {
            Username = dto.Username,
            PasswordHash = hashed
        };

        try
        {
            await _usuarioService.AdicionarAsync(user);
        }
        catch (Exception)
        {

            throw new Exception($"O Username: {user.Username} ja existe!");
        }
        

        return new UsuarioDTO
        {
            Id = user.Id,
            Username = user.Username
        };
    }

    public bool validaUsername(string username)
    {
        Usuario user = _usuarioService.ObterPorUsername(username).Result;
        if (user == null) 
            return false;

        return true;
    }

    public async Task<TokenDto> LoginAsync(UsuarioDTO dto)
    {
        var user = await _usuarioService.ObterPorUsername(dto.Username)
                   ?? throw new UnauthorizedAccessException("Usuário ou senha inválidos");

        if (!BCrypt.Net.BCrypt.Verify(dto.PasswordHash, user.PasswordHash))
            throw new UnauthorizedAccessException("Usuário ou senha inválidos");

        
        var jwtSection = _configuration.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwtSection["Key"]!);
        var issuer = jwtSection["Issuer"];
        var audience = jwtSection["Audience"];
        var expiresIn = int.Parse(jwtSection["ExpiresInMinutes"]!);

        
        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim("uid", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

        var creds = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256);

        var expires = DateTime.UtcNow.AddMinutes(expiresIn);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expires,
            signingCredentials: creds
        );

        var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
        return new TokenDto
        {
            Token = tokenStr,
            Expires = expires
        };
    }
}
