using AutoMapper;
using eSiafApiN4.DTOs;
using eSiafApiN4.Entidades;

namespace eSiafApiN4.Utilidades;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<GeneroDtoUpsert, Genero>();
        CreateMap<Genero, GeneroDto>();
    }
}