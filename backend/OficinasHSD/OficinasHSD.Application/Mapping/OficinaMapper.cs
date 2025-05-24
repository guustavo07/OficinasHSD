using AutoMapper;
using OficinasHSD.Application.DTOs;
using OficinasHSD.Domain.Models;

namespace OficinasHSD.Application.Mapping;

public class OficinaMapper : Profile
{
    public OficinaMapper()
    {
        CreateMap<OficinaMecanicaDTO, OficinaMecanica>();


        CreateMap<OficinaMecanica, OficinaMecanicaDTO>();
    }
}