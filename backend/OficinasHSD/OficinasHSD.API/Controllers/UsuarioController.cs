using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OficinasHSD.Application.DTOs;
using OficinasHSD.Application.Interfaces.Servicos;
using OficinasHSD.Domain.Models;

namespace OficinasHSD.API.Controllers;

[Route("[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _service;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    public UsuarioController(IUsuarioService service, IAuthService authService, IMapper mapper)
    {   
        _service = service;
        _mapper = mapper;
        _authService = authService;
    }


    [HttpPost("Registrar")]
    public async Task<IActionResult> RegistrarUsuario([FromBody] UsuarioDTO dto)
    {
        try
        {
            bool existeUsername = _authService.validaUsername(dto.Username);
            if (existeUsername)
            {
                return Conflict(new { error = $"O username '{dto.Username}' já está em uso." });
            }

            await _authService.RegistrarUsuarioAsync(dto);
            return Ok(new {message = "Registrado com sucesso"});
        }
        catch (Exception ex)
        {

            throw;
        }
        
    }
    
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UsuarioDTO dto)
    {
        try
        {
            TokenDto tokenDTO = await _authService.LoginAsync(dto);
            return Ok(tokenDTO);
        }
        catch (UnauthorizedAccessException)
        {

            return BadRequest(new { error = "Usuário ou senha inválidos." });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { error = "Ocorreu um erro interno. Tente novamente mais tarde." });
        }
    }
}
