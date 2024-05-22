namespace eSiafApiN4.Entidades.XanesN4;

public class QuotationDetailList
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string TypeCode { get; set; } = null!;
    public int TypeNumeral { get; set; }
    public short TypeDetail { get; set; }
    public int HeaderNumeral { get; set; }
    public int DetailNumeral { get; set; }
    public string CurrencyDetailCode { get; set; } = null!;
    public string? BankSourceCode { get; set; }
    public string? BankAccountSourceCode { get; set; }
    public string? BankTargetCode { get; set; }
    public string? BankAccountTargetCode { get; set; }
    public decimal AmountDetail { get; set; }
    public decimal? AmountTransferFee { get; set; }
    public Guid? JournalEntryUId { get; set; }
    public Guid? TransactionRelateUId { get; set; }
    public Guid? JournalEntryTransferFeeId { get; set; }
    public Guid? BankTransactionTransferFeeId { get; set; }

}