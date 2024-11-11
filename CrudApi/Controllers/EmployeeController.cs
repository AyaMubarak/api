using CrudApi.Data;
using CrudApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public EmployeeController(ApplicationDbContext context) {
            this.context = context;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var employees=context.Employees.ToList();
            return Ok(employees);
        } 
        [HttpGet("Details")]
        public IActionResult GetById(int id)
        {
            var employee=context.Employees.Find(id);
            if (employee == null) {
                return NotFound("Employee is not Found");
            }

            return Ok(employee);
        } 
        [HttpPost("Create")]
        public IActionResult Create(Employee employee)
        {
           context.Employees.Add(employee);
            context.SaveChanges();


            return Ok();
        }
        [HttpGet("Update")]
        public IActionResult Update(int id,Employee employee)
        {
            var current = context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound("Employee is not Found");
            }
            current.Name = employee.Name;
            current.Description = employee.Description;
            context.SaveChanges();

            return Ok(employee);
        }
        [HttpDelete("Remove")]
        public IActionResult Remove(int id) {
            var employee = context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound("employee is not found");
            }
            context.Employees.Remove(employee);
            context.SaveChanges();
            return Ok(employee);
        }

    }
}
