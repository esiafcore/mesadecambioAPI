namespace eSiafApiN4.Entidades.XanesN4;

public class QuotationHeaderList
{
    public int Id { get; set; }
    public string TypeCode { get; set; } = null!;
    public int NumeralTrx { get; set; }
    public DateTime DateTrx { get; set; }
    public string CustomerCode { get; set; } = null!;
    public string CustomerIdentificationNumber{ get; set; } = null!;
    public string BusinessName { get; set; } = null!;
    public int CurrencyTransaction { get; set;}
    public int CurrencyDeposit { get; set; }
    public int CurrencyTransfer { get; set; }
    public decimal ExchangeRateOfficial { get; set; }
    public decimal ExchangeRateBuy { get; set; }
    public decimal ExchangeRateSell { get; set; }
    public decimal AmountTrx { get; set; }
    public decimal AmountExchange { get; set; }
    public decimal AmountCost { get; set; }
    public decimal AmountRevenue { get; set; }
    public decimal AmountTransferFee{ get; set; }
    public string? BankSourceCode { get; set; }
    public int? BankAccountSourceId { get; set; }
    public string? BankAccountSourceCode { get; set; }
    public string? BankTargetCode { get; set; }
    public int? BankAccountTargetId { get; set; }
    public string? BankAccountTargetCode { get; set; }
    public string BusinessExecutiveCode { get; set; } = null!;
    public bool IsClosed { get; set; }
    public bool IsJournalPost { get; set; }
    public bool IsVoid { get; set; }

    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? ClosedOn { get; set; }
    public string? ClosedBy { get; set; }
    public DateTime? ReClosedOn { get; set; }
    public string? ReClosedBy { get; set; }
}

