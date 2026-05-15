namespace AIEnterpriseOS.Dashboard.Service.Models;

public class Widget
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Type { get; set; } = string.Empty; // Finance.KPI, Project.Progress, ecc.
    public string Title { get; set; } = string.Empty;
    public Dictionary<string, string> Parameters { get; set; } = new();
}

public class DashboardLayout
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; } = string.Empty;
    public List<Widget> Widgets { get; set; } = new();
}
