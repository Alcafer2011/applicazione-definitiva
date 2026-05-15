using AIEnterpriseOS.Manufacturing.Service.Models;

namespace AIEnterpriseOS.Manufacturing.Service.BOM;

public interface IBomEngine
{
    BillOfMaterials Create(BillOfMaterials bom);
    BillOfMaterials? Get(string id);
}

public class InMemoryBomEngine : IBomEngine
{
    private readonly List<BillOfMaterials> _boms = new();

    public BillOfMaterials Create(BillOfMaterials bom)
    {
        _boms.Add(bom);
        return bom;
    }

    public BillOfMaterials? Get(string id)
        => _boms.FirstOrDefault(x => x.Id == id);
}
