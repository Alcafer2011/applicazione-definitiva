using Microsoft.AspNetCore.Mvc;
using AIEnterpriseOS.LoadPlanner3D.Service.Models;

namespace AIEnterpriseOS.LoadPlanner3D.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoadPlanner3DController : ControllerBase
{
    [HttpPost("generate")]
    public IActionResult GeneratePlan([FromBody] List<LoadItem3D> items)
    {
        // TODO: algoritmo AI di packing 3D
        double currentX = 0;

        foreach (var item in items)
        {
            item.X = currentX;
            item.Y = 0;
            item.Z = 0;
            currentX += item.LengthMm;
        }

        var plan = new LoadPlan3D
        {
            Vehicle = new Vehicle3D
            {
                Type = "truck_tautliner",
                LengthMm = 13600,
                WidthMm = 2450,
                HeightMm = 2700
            },
            Items = items
        };

        return Ok(plan);
    }

    [HttpPost("note")]
    public IActionResult AddNote([FromBody] LoadItem3D item)
    {
        // TODO: persistenza note
        return Ok(new { status = "saved", item.Id, item.Note });
    }
}
