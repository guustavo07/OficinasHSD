using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OficinasHSD.Application.DTOs;
using OficinasHSD.Application.Interfaces.Servicos;

namespace OficinasHSD.API.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class OficinaController : ControllerBase
{
    private readonly IOficinaService _service;
    public OficinaController(IOficinaService service)
    {
        _service = service;
    }

    [HttpGet("ObterTodos")]
    public async Task<IActionResult> ObterTodos()
    {
        IEnumerable<OficinaMecanicaDTO> oficinas = await _service.ObterTodosAsync();
        return Ok(oficinas);
    }

    [HttpGet("ObterPorId/{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        OficinaMecanicaDTO oficina = await _service.ObterPorIdAsync(id);
        if (oficina is null)
            return NotFound();

        return Ok(oficina);
    }

    [HttpPost("Inserir")]
    public async Task<IActionResult> Inserir([FromBody] OficinaMecanicaDTO dto)
    {
        await _service.AdicionarAsync(dto);
        return Ok("Oficina inserida com sucesso");
    }
    
    [HttpPut("Atualizar/{id:guid}")]
    public async Task<IActionResult> Atualizar(OficinaMecanicaDTO oficina)
    {
        await _service.Atualizar(oficina);
        return Ok("Oficina atualizada com sucesso");
    }
    
    [HttpDelete("Deletar/{id:guid}")]
    public async Task<IActionResult> Remover(Guid id)
    {
        await _service.Remover(id);
        return Ok("Oficina removida com sucesso");
    }


}