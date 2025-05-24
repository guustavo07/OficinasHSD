using AutoMapper;
using OficinasHSD.Application.DTOs;
using OficinasHSD.Application.Interfaces.Repositorios;
using OficinasHSD.Application.Interfaces.Servicos;
using OficinasHSD.Domain.Models;

namespace OficinasHSD.Application.Services;

public class UsuarioService : IUsuarioService
{ 
    protected readonly IUsuarioRepository _repository;
    private readonly IMapper _mapper;
    public UsuarioService(IUsuarioRepository repository, IMapper mapper) 
    {
        _repository = repository;
        _mapper = mapper;
    }

    public virtual async Task AdicionarAsync(UsuarioDTO entity)
    {
        try
        {
            await _repository.AdicionarAsync(_mapper.Map<Usuario>(entity));
        }
        catch (Exception e)
        {
            throw new Exception($"Erro ao gravar {entity.GetType().Name} - {e}");
        }
    }

    public async Task<Usuario> ObterPorUsername(string username)
    {
        return await _repository.ObterPorUsername(username);
    }
}