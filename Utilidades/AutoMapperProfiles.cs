using AutoMapper;
using XanesN8.Api.DTOs.eSiafN4;
using XanesN8.Api.DTOs.XanesN4;
using XanesN8.Api.Entidades.eSiafN4;
using XanesN8.Api.Entidades.XanesN4;

namespace XanesN8.Api.Utilidades;

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

    }
}