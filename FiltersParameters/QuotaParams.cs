namespace eSiafApiN4.FiltersParameters;

public class QuotaParams
{
    public int CompanyId { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? IdentificationNumber { get; set; }
    public int Pagina { get; set; }
    public int RecordsPorPagina { get; set; }
}