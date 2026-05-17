using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using HR.Service.Models;

namespace HR.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMongoClient _mongoClient;
        private readonly IMongoCollection<Employee> _employees;

        public EmployeesController(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
            var db = mongoClient.GetDatabase("hr_service");
            _employees = db.GetCollection<Employee>("employees");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var employees = await _employees.Find(e => true).ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            await _employees.InsertOneAsync(employee);
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(string id)
        {
            var employee = await _employees.Find(e => e.Id == id).FirstOrDefaultAsync();
            if (employee == null)
                return NotFound();
            return Ok(employee);
        }
    }
}
