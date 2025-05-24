namespace OficinasHSD.Application.DTOs;

public class OficinaMecanicaDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public string Servicos { get; set; } = string.Empty;
}
