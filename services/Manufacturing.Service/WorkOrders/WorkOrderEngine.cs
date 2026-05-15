using AIEnterpriseOS.Manufacturing.Service.Models;

namespace AIEnterpriseOS.Manufacturing.Service.WorkOrders;

public interface IWorkOrderEngine
{
    WorkOrder Create(WorkOrder wo);
    WorkOrder? Get(string id);
    WorkOrder UpdateStatus(string id, WorkOrderStatus status);
}

public class InMemoryWorkOrderEngine : IWorkOrderEngine
{
    private readonly List<WorkOrder> _orders = new();

    public WorkOrder Create(WorkOrder wo)
    {
        _orders.Add(wo);
        return wo;
    }

    public WorkOrder? Get(string id)
        => _orders.FirstOrDefault(x => x.Id == id);

    public WorkOrder UpdateStatus(string id, WorkOrderStatus status)
    {
        var wo = _orders.First(x => x.Id == id);
        wo.Status = status;
        return wo;
    }
}
