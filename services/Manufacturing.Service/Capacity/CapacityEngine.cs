namespace AIEnterpriseOS.Manufacturing.Service.Capacity;

public interface ICapacityEngine
{
    int EstimateProductionTime(string product, int qty);
}

public class BasicCapacityEngine : ICapacityEngine
{
    public int EstimateProductionTime(string product, int qty)
    {
        return qty * 15; // 15 min per unit (placeholder)
    }
}
