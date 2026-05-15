namespace AIEnterpriseOS.VerticalPack.Service.Models;

public class VerticalPack
{
    public string Code { get; set; } = string.Empty; // es: ARTIGIANO
    public string Name { get; set; } = string.Empty;
    public List<string> EnabledModules { get; set; } = new();
    public List<string> DefaultWorkflows { get; set; } = new();
    public List<string> DefaultDashboards { get; set; } = new();
    public List<string> AiPrompts { get; set; } = new();
}
