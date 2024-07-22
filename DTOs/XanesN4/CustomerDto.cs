namespace XanesN8.Api.DTOs.XanesN4;

public class CustomerDto
{
    public int Id { get; set; }
    public string PersonType { get; set; } = null!;
    public string SectorCategoryCode { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string IdentificationTypeCode { get; set; } = null!;
    public string IdentificationNumber { get; set; } = null!;
    public string? BusinessName { get; set; }
    public string? CommercialName { get; set; }
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
    public string? LastName { get; set; }
    public string? SecondSurname { get; set; }
    public string? BusinessAddress { get; set; }
    public bool IsBank { get; set; }
    public bool IsSystemRow { get; set; }
    public int TotalQuotations { get; set; }

}