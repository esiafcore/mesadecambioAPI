namespace eSiafApiN4.DTOs;

public class PaginacionDto
{
    public int Pagina { get; set; } = 1;
    private int _recordsPorPagina = 15;
    private readonly int _cantidadMaximaRecordsPorPagina = 50;

	public int RecordsPorPagina
    {
		get => _recordsPorPagina;
        set {
            if (value != 0)
            {
				_recordsPorPagina = (value > _cantidadMaximaRecordsPorPagina) ?
				_cantidadMaximaRecordsPorPagina : value;
			}
		}
	}
}