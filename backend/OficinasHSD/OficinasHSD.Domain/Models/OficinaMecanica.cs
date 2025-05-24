using System.ComponentModel.DataAnnotations.Schema;

namespace OficinasHSD.Domain.Models;

[Table("TbOficinaMecanica")]
public class OficinaMecanica 
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public string Servicos { get; set; } = string.Empty;
}
