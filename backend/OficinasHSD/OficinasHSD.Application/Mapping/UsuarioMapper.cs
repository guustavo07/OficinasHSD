using AutoMapper;
using OficinasHSD.Application.DTOs;
using OficinasHSD.Domain.Models;

namespace OficinasHSD.Application.Mapping;

public class UsuarioMapper : Profile
{
    public UsuarioMapper()
    {
        CreateMap<UsuarioDTO, Usuario>();


        CreateMap<Usuario, UsuarioDTO>();
    }
}