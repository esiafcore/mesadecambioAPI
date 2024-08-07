namespace XanesN8.Api.Entidades.XanesN8;

public class TransfersList
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public int CompanyId { get; set; }
    public int TypeNumeral { get; set; }
    public int QuotationDetailtype { get; set; }
    public DateTime DateTransa { get; set; }
    public string FullTransaNumeral { get; set; } = null!;
    public int DetailLineNumeral { get; set; }
    public string BusinessName { get; set; } = null!;
    public string BusinessExecutiveCode { get; set; } = null!;
    public string CurrencyTransactionCode { get; set; } = null!;
    public string CurrencyTransferCode { get; set; } = null!;
    public string BankCodeSource { get; set; } = null!;
    public string BankCodeTarget { get; set; } = null!;
    public decimal ExchangeRateReal { get; set; }
    public decimal ExchangeRateTransa { get; set; }
    public decimal ExchangeRateOfficialTransa { get; set; }
    public decimal ExchangeRateOfficialReal { get; set; }
    public decimal AmountTransaction { get; set; }
    public decimal AmountTransferTotal { get; set; }
    public decimal AmountTransferDetail { get; set; }
    public decimal AmountCommission { get; set; }

    public bool IsClosed { get; set; }
    public bool IsBank { get; set; }
    public bool IsPayment { get; set; }
    public bool IsLoan { get; set; }
    public bool IsVoid { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string ClosedBy { get; set; } = null!;
}
