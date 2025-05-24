using OficinasHSD.Application.DTOs;

namespace OficinasHSD.Application.Interfaces.Servicos;

public interface IOficinaService
{ 
    Task<OficinaMecanicaDTO?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<OficinaMecanicaDTO>> ObterTodosAsync();
    Task AdicionarAsync(OficinaMecanicaDTO oficina);
    Task Atualizar(OficinaMecanicaDTO oficina);
    Task Remover(Guid id);
}