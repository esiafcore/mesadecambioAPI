namespace XanesN8.Api.Entidades.XanesN8;

public class DepositsList
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
    public string CurrencyDepositCode { get; set; } = null!;
    public string BankCode { get; set; } = null!;
    public decimal ExchangeRateTransa { get; set; }
    public decimal ExchangeRateReal { get; set; }
    public decimal ExchangeRateOfficialTransa { get; set; }
    public decimal ExchangeRateOfficialReal { get; set; }
    public decimal AmountTransaction { get; set; }
    public decimal AmountDepositTotal { get; set; }
    public decimal AmountDepositDetail { get; set; }
    public bool IsClosed { get; set; }
    public bool IsBank { get; set; }
    public bool IsPayment { get; set; }
    public bool IsLoan { get; set; }
    public bool IsVoid { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string ClosedBy { get; set; } = null!;


}
