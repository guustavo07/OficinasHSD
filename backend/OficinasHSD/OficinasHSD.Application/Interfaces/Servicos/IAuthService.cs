using OficinasHSD.Application.DTOs;
using OficinasHSD.Domain.Models;

namespace OficinasHSD.Application.Interfaces.Servicos;
public interface IAuthService
{
    Task<UsuarioDTO> RegistrarUsuarioAsync(UsuarioDTO usuario);
    Task<TokenDto> LoginAsync(UsuarioDTO usuario);
    bool validaUsername(string username);
}