using AIEnterpriseOS.Dashboard.Service.Models;

namespace AIEnterpriseOS.Dashboard.Service.Widgets;

public interface IWidgetEngine
{
    object Render(Widget widget);
}

public class WidgetEngine : IWidgetEngine
{
    public object Render(Widget widget)
    {
        return widget.Type switch
        {
            "Finance.Revenue" => new { value = 120000, label = "Revenue (YTD)" },
            "Finance.Overdue" => new { value = 12, label = "Fatture Scadute" },
            "Project.Progress" => new { value = 68, label = "Avanzamento Progetti" },
            "Customer.NewMessages" => new { value = 5, label = "Nuovi Messaggi" },
            "Scenario.Impact" => new { value = "+12%", label = "Impatto Scenario" },
            _ => new { value = "N/A", label = "Unknown Widget" }
        };
    }
}
