using AutoMapper;
using OficinasHSD.Application.DTOs;
using OficinasHSD.Application.Interfaces.Repositorios;
using OficinasHSD.Application.Interfaces.Servicos;
using OficinasHSD.Domain.Models;

namespace OficinasHSD.Application.Services;

public class OficinaService : IOficinaService
{
    protected readonly IOficinaRepository _repository;
    private readonly IMapper _mapper;
    public OficinaService(IOficinaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public virtual async Task AdicionarAsync(OficinaMecanicaDTO entity)
    {
        try
        {
            await _repository.AdicionarAsync(_mapper.Map<OficinaMecanica>(entity));
        }
        catch (Exception e)
        {
            throw new Exception($"Erro ao gravar {entity.GetType().Name} - {e}");
        }
    }

    public virtual async Task Atualizar(OficinaMecanicaDTO oficinaAtualizada)
    {
        try
        {
            OficinaMecanica oficina = await _repository.ObterPorIdAsync(oficinaAtualizada.Id);
            if(oficina == null)
                throw new InvalidOperationException($"Oficina com ID {oficinaAtualizada.Id} não encontrada");
            
            _mapper.Map(oficinaAtualizada, oficina);

            await _repository.Atualizar(oficina);
        }
        catch (Exception e)
        {
            throw new Exception($"Erro ao atualizar - {e.Message}", e);
        }
    }


    public virtual async Task<OficinaMecanicaDTO?> ObterPorIdAsync(Guid id)
    {
        var entity = await _repository.ObterPorIdAsync(id);
        if (entity == null)
           throw new InvalidOperationException("Oficina não encontrada");
        
        return _mapper.Map<OficinaMecanicaDTO>(entity);
    }

    public virtual async Task<IEnumerable<OficinaMecanicaDTO>> ObterTodosAsync()
    {
        return _mapper.Map<IEnumerable<OficinaMecanicaDTO>>(await _repository.ObterTodosAsync()); 
    }

    public virtual async Task Remover(Guid id)
    {
        try
        {
            await _repository.Remover(id);
        }
        catch (Exception e)
        {
            throw new Exception($"Erro ao remover - {e}");
        }
    }

    public void Dispose()
    {
        _repository.Dispose();
    }

}