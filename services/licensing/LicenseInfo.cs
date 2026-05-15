namespace AIEnterpriseOS.Licensing;

public class LicenseInfo
{
    public string LicenseId { get; set; } = "";
    public string CustomerName { get; set; } = "";
    public string VatNumber { get; set; } = "";
    public string Country { get; set; } = "";
    public string MachineId { get; set; } = "";
    public string[] MacAddresses { get; set; } = System.Array.Empty<string>();
    public string[] Modules { get; set; } = System.Array.Empty<string>();
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public int MaxUsers { get; set; }
    public bool MultiLanguage { get; set; }
    public bool CountryAutoConfig { get; set; }
    public string Signature { get; set; } = "";
}
