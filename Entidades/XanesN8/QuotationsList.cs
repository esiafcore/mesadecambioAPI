namespace XanesN8.Api.Entidades.XanesN8;

public class QuotationsList
{
    public int Id { get; set; }
    public int CompanyId { get; set; }

    public int TypeNumeral { get; set; }
    public DateTime DateTransa { get; set; }
    public string FullTransaNumeral { get; set; } = null!;
    public string BusinessName { get; set; } = null!;
    public string BusinessExecutiveCode { get; set; } = null!;
    public string CurrencyTransactionCode { get; set; } = null!;
    public string CurrencyTransferCode { get; set; } = null!;
    public string CurrencyDepositCode { get; set; } = null!;
    public decimal ExchangeRateTrx { get; set; }
    public decimal AmountTransaction { get; set; }
    public decimal AmountExchange { get; set; }
    public decimal TotalDeposit { get; set; }
    public decimal TotalTransfer { get; set; }
    public decimal ExchangeRateOfficialReal { get; set; }
    public decimal AmountCost { get; set; }
    public decimal AmountRevenue { get; set; }
    public bool IsClosed { get; set; }
    public bool IsBank { get; set; }
    public bool IsPayment { get; set; }
    public bool IsLoan { get; set; }
    public bool IsVoid { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string ClosedBy { get; set; } = null!;


}