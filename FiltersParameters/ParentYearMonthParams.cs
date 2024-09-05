namespace XanesN8.Api.FiltersParameters;

public class ParentYearMonthParams
{
    public Guid UidParent { get; set; }
    public Guid Uidcia { get; set; }
    public int Yearfiscal { get; set; }
    public int Mesfiscal { get; set; }
    public int Pagina { get; set; }
    public int RecordsPorPagina { get; set; }
}