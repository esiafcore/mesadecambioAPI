﻿using AutoMapper;
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
        CreateMap<Bancos, BancosDto>().ReverseMap();
        CreateMap<BancosDtoCreate, Bancos>().ReverseMap();

        CreateMap<TransaccionesBco, TransaccionesBcoDto>().ReverseMap();
        CreateMap<TransaccionesBco, TransaccionesBcoDtoCreate>().ReverseMap();
        CreateMap<TransaccionesBco, TransaccionesBcoDtoUpdate>().ReverseMap();

        CreateMap<TransaccionesBcoDetalle, TransaccionesBcoDetalleDto>().ReverseMap();
        CreateMap<TransaccionesBcoDetalle, TransaccionesBcoDetalleDtoCreate>().ReverseMap();
        CreateMap<TransaccionesBcoDetalle, TransaccionesBcoDetalleDtoUpdate>().ReverseMap();
        
        CreateMap<ConfigBco, ConfigBcoDto>().ReverseMap();
        CreateMap<ConfigBco, ConfigBcoDtoUpdate>().ReverseMap();

        CreateMap<ConsecutivosBco, ConsecutivosBcoDto>().ReverseMap();
        CreateMap<ConsecutivosBco, ConsecutivosBcoDtoUpdate>().ReverseMap();

        CreateMap<ConsecutivosBcoDetalle, ConsecutivosBcoDetalleDto>().ReverseMap();
        CreateMap<ConsecutivosBcoDetalle, ConsecutivosBcoDetalleDtoUpdate>().ReverseMap();

        CreateMap<CuentasBancarias, CuentasBancariasDto>().ReverseMap();
        CreateMap<CuentasBancarias, CuentasBancariasDtoUpdate>().ReverseMap();

        CreateMap<TransaccionesBcoTipos, TransaccionesBcoTipoDto>().ReverseMap();
        CreateMap<TransaccionesBcoTipos, TransaccionesBcoTipoDtoCreate>().ReverseMap();
        CreateMap<TransaccionesBcoTipos, TransaccionesBcoTipoDtoUpdate>().ReverseMap();

        CreateMap<TransaccionesBcoSubtipos, TransaccionesBcoSubtipoDto>().ReverseMap();
        CreateMap<TransaccionesBcoSubtipos, TransaccionesBcoSubtipoDtoCreate>().ReverseMap();
        CreateMap<TransaccionesBcoSubtipos, TransaccionesBcoSubtipoDtoUpdate>().ReverseMap();

        CreateMap<AsientosContables, AsientosContablesDto>().ReverseMap();
        CreateMap<AsientosContables, AsientosContablesDtoCreate>().ReverseMap();
        CreateMap<AsientosContables, AsientosContablesDtoUpdate>().ReverseMap();

        CreateMap<AsientosContablesDetalle, AsientosContablesDetalleDto>().ReverseMap();
        CreateMap<AsientosContablesDetalle, AsientosContablesDetalleDtoCreate>().ReverseMap();
        CreateMap<AsientosContablesDetalle, AsientosContablesDetalleDtoUpdate>().ReverseMap();

        CreateMap<ConfigCnt, ConfigCntDto>().ReverseMap();
        CreateMap<ConfigCnt, ConfigCntDtoUpdate>().ReverseMap();

        CreateMap<ConsecutivosCnt, ConsecutivosCntDto>().ReverseMap();
        CreateMap<ConsecutivosCnt, ConsecutivosCntDtoUpdate>().ReverseMap();

        CreateMap<ConsecutivosCntDetalle, ConsecutivosCntDetalleDto>().ReverseMap();
        CreateMap<ConsecutivosCntDetalle, ConsecutivosCntDetalleDtoUpdate>().ReverseMap();

        CreateMap<Modulos, ModulosDto>().ReverseMap();

        CreateMap<ModulosDocumentos, ModulosDocumentosDto>().ReverseMap();


        //XanesN4
        CreateMap<Customer, CustomerDto>().ReverseMap();
    }
}