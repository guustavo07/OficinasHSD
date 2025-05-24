using OficinasHSD.Application.DTOs;
using OficinasHSD.Domain.Models;

namespace OficinasHSD.Application.Interfaces.Repositorios;

public interface IOficinaRepository
{
    Task<OficinaMecanica?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<OficinaMecanica>> ObterTodosAsync();
    Task AdicionarAsync(OficinaMecanica entity);
    Task Atualizar(OficinaMecanica entity);
    Task Remover(Guid id);
    void Dispose();
}