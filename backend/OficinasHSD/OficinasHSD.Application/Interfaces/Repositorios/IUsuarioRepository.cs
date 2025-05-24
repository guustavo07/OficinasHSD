using OficinasHSD.Domain.Models;

namespace OficinasHSD.Application.Interfaces.Repositorios;
public interface IUsuarioRepository
{
    Task<Usuario> ObterPorUsername(string username);
    Task AdicionarAsync(Usuario entity);
}
