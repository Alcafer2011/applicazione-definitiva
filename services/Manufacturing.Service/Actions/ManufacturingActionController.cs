using Microsoft.AspNetCore.Mvc;
using AIEnterpriseOS.Manufacturing.Service.Models;
using AIEnterpriseOS.Manufacturing.Service.WorkOrders;
using AIEnterpriseOS.Manufacturing.Service.Capacity;

namespace AIEnterpriseOS.Manufacturing.Service.Actions;

[ApiController]
[Route("api/manufacturing/action")]
public class ManufacturingActionController : ControllerBase
{
    private readonly IWorkOrderEngine _wo;
    private readonly ICapacityEngine _capacity;

    public ManufacturingActionController(IWorkOrderEngine wo, ICapacityEngine capacity)
    {
        _wo = wo;
        _capacity = capacity;
    }

    [HttpPost("create-wo")]
    public IActionResult CreateWO([FromBody] WorkOrder wo)
    {
        return Ok(_wo.Create(wo));
    }

    [HttpPost("update-status/{id}/{status}")]
    public IActionResult UpdateStatus(string id, WorkOrderStatus status)
    {
        return Ok(_wo.UpdateStatus(id, status));
    }

    [HttpGet("estimate/{product}/{qty}")]
    public IActionResult Estimate(string product, int qty)
    {
        return Ok(new { minutes = _capacity.EstimateProductionTime(product, qty) });
    }
}
