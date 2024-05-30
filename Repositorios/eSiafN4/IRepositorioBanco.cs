﻿using eSiafApiN4.Entidades;
using eSiafApiN4.Entidades.eSiafN4;
using eSiafApiN4.FiltersParameters;

namespace eSiafApiN4.Repositorios.eSiafN4;

public interface IRepositorioBanco
{
    Task<List<Bancos>> GetAlls(QueryParams queryParams);
    Task<Bancos?> GetById(Guid id);
    Task<Guid> Create(Bancos objNew);
    Task Delete(Guid id);
    Task<bool> Exist(Guid id, string code);

}