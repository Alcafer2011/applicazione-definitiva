using Microsoft.AspNetCore.Mvc;
using AIEnterpriseOS.CustomerHub.Service.Models;
using AIEnterpriseOS.CustomerHub.Service.Services;

namespace AIEnterpriseOS.CustomerHub.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerHubController : ControllerBase
{
    private readonly ICustomerHubEngine _engine;

    public CustomerHubController(ICustomerHubEngine engine)
    {
        _engine = engine;
    }

    [HttpPost("customer")]
    public IActionResult CreateCustomer([FromBody] Customer customer)
    {
        return Ok(_engine.CreateCustomer(customer));
    }

    [HttpGet("customer/{id}")]
    public IActionResult GetCustomer(string id)
    {
        var c = _engine.GetCustomer(id);
        if (c is null) return NotFound();
        return Ok(c);
    }

    [HttpGet("customers")]
    public IActionResult GetAllCustomers()
    {
        return Ok(_engine.GetAllCustomers());
    }

    [HttpPost("message")]
    public IActionResult AddMessage([FromBody] CustomerMessage msg)
    {
        return Ok(_engine.AddMessage(msg));
    }

    [HttpGet("messages/{customerId}")]
    public IActionResult GetMessages(string customerId)
    {
        return Ok(_engine.GetMessages(customerId));
    }

    [HttpGet("timeline/{customerId}")]
    public IActionResult GetTimeline(string customerId)
    {
        return Ok(_engine.GetTimeline(customerId));
    }
}
