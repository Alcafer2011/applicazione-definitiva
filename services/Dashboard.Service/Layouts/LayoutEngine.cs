using AIEnterpriseOS.Dashboard.Service.Models;

namespace AIEnterpriseOS.Dashboard.Service.Services;

public interface ILayoutEngine
{
    DashboardLayout Save(DashboardLayout layout);
    DashboardLayout? Get(string userId);
}

public class InMemoryLayoutEngine : ILayoutEngine
{
    private readonly List<DashboardLayout> _layouts = new();

    public DashboardLayout Save(DashboardLayout layout)
    {
        var existing = _layouts.FirstOrDefault(x => x.UserId == layout.UserId);
        if (existing != null)
            _layouts.Remove(existing);

        _layouts.Add(layout);
        return layout;
    }

    public DashboardLayout? Get(string userId)
        => _layouts.FirstOrDefault(x => x.UserId == userId);
}
