using AutoMapper;
using eSiafApiN4.DTOs;
using eSiafApiN4.DTOs.eSiafN4;
using eSiafApiN4.Entidades;
using eSiafApiN4.Entidades.eSiafN4;

namespace eSiafApiN4.Utilidades;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<GeneroDtoCreate, Genero>();
        CreateMap<GeneroDtoUpdate, Genero>();
        CreateMap<GeneroDtoCreate, GeneroDtoUpdate>();
        CreateMap<Genero, GeneroDto>();

        CreateMap<AsientosContables, AsientosContablesDto>();
        CreateMap<Bancos, BancosDto>();
        CreateMap<TransaccionesBco, TransaccionesBcoDto>();
        CreateMap<CuentasBancarias, CuentasBancariasDto>();

    }
}