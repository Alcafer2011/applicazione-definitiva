using Microsoft.AspNetCore.Mvc;
using Workflow.Service.Models;

namespace Workflow.Service.Controllers;

[ApiController]
[Route("workflow")]
public class WorkflowController : ControllerBase
{
    private static readonly List<WorkflowDefinition> Definitions = new();
    private static readonly List<WorkflowInstance> Instances = new();

    [HttpPost("definitions")]
    public IActionResult CreateDefinition(WorkflowDefinition def)
    {
        Definitions.Add(def);
        return Ok(def);
    }

    [HttpPost("instances")]
    public IActionResult CreateInstance(string definitionId)
    {
        var def = Definitions.FirstOrDefault(d => d.Id == definitionId);
        if (def == null) return NotFound();

        var instance = new WorkflowInstance
        {
            DefinitionId = def.Id,
            CurrentState = def.States.First()
        };

        Instances.Add(instance);
        return Ok(instance);
    }

    [HttpPost("transition")]
    public IActionResult Transition(string instanceId, string trigger)
    {
        var inst = Instances.FirstOrDefault(i => i.Id == instanceId);
        if (inst == null) return NotFound();

        var def = Definitions.First(d => d.Id == inst.DefinitionId);

        var t = def.Transitions.FirstOrDefault(x => x.From == inst.CurrentState && x.Trigger == trigger);
        if (t == null) return BadRequest("Transizione non valida");

        inst.CurrentState = t.To;
        return Ok(inst);
    }

    [HttpGet("instances/{id}")]
    public IActionResult GetInstance(string id)
    {
        var inst = Instances.FirstOrDefault(i => i.Id == id);
        return inst == null ? NotFound() : Ok(inst);
    }
}