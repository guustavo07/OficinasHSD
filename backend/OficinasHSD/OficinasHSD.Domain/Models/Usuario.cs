using System.ComponentModel.DataAnnotations.Schema;

namespace OficinasHSD.Domain.Models;

[Table("TbUsuario")]
public class Usuario
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}
