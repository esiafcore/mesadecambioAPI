namespace eSiafApiN4.FiltersParameters;

public class DatesParams
{
    public int CompanyId { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Pagina { get; set; }
    public int RecordsPorPagina { get; set; }
}