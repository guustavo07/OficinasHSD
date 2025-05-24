using OficinasHSD.Application.DTOs;
using OficinasHSD.Domain.Models;

namespace OficinasHSD.Application.Interfaces.Servicos;
public interface IUsuarioService 
{
    Task<Usuario> ObterPorUsername(string username);
    Task AdicionarAsync(UsuarioDTO entity);
}
