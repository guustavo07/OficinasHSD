

namespace OficinasHSD.Application.DTOs;

public class UsuarioDTO
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}
