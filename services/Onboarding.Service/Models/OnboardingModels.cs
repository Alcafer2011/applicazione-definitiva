namespace AIEnterpriseOS.Onboarding.Service.Models;

public class CompanyProfile
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CompanyName { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public string VerticalCode { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class ImportRequest
{
    public string Type { get; set; } = string.Empty; // customers, products, price_list
    public string Base64File { get; set; } = string.Empty;
}
