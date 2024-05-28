using AutoMapper;
using eSiafApiN4.DTOs;
using eSiafApiN4.DTOs.eSiafN4;
using eSiafApiN4.DTOs.XanesN4;
using eSiafApiN4.Entidades;
using eSiafApiN4.Entidades.eSiafN4;
using eSiafApiN4.Entidades.XanesN4;

namespace eSiafApiN4.Utilidades;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        //eSiafN4
        CreateMap<AsientosContables, AsientosContablesDto>();
        CreateMap<Bancos, BancosDto>();
        CreateMap<BancosDtoCreate, Bancos>();
        CreateMap<TransaccionesBco, TransaccionesBcoDto>();
        CreateMap<CuentasBancarias, CuentasBancariasDto>();

        //XanesN4
        CreateMap<Customer, CustomerDto>();

        //Shared
        CreateMap<GeneroDtoCreate, Genero>();
        CreateMap<GeneroDtoUpdate, Genero>();
        CreateMap<GeneroDtoCreate, GeneroDtoUpdate>();
        CreateMap<Genero, GeneroDto>();

    }
}