using CrudApi.Data;
using CrudApi.DTOs.Department;
using CrudApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;


namespace CrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public DepartmentController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var departments = context.Departments.Select(
                x => new GetDepartmentDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                }
                );
            return Ok(departments);
        }


        [HttpGet("Details")]
        public IActionResult GetById(int id)
        {
            var department = context.Departments
                                     .Where(x => x.Id == id)
                                     .Select(x => new GetDepartmentDTO
                                     {
                                         Id = x.Id,
                                         Name = x.Name
                                     })
                                     .FirstOrDefault();

            if (department == null)
            {
                return NotFound("Department is not found");
            }

            return Ok(department);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateDepartmentDTo depDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Department department = new Department()
            {
                Name = depDto.Name,
            };

            await context.Departments.AddAsync(department);
            await context.SaveChangesAsync();

            return Ok(depDto);
        }

        [HttpPut("Update")]
        public IActionResult Update(int id, UpdateDepartmentDTO depDto)
        {
            var current = context.Departments
                                  .Where(x => x.Id == id) 
                                  .FirstOrDefault(); 

            if (current == null)
            {
                return NotFound("Department is not found");
            }

            current.Name = depDto.Name; 
            context.SaveChanges();

            return Ok(new GetDepartmentDTO
            {
                Id = current.Id,
                Name = current.Name
            });

        }
        [HttpDelete("Remove")]
        public IActionResult Remove(int id)
        {
            var department = context.Departments
                                    .Where(x => x.Id == id)
                                    .FirstOrDefault(); 

            if (department == null)
            {
                return NotFound("Department is not found");
            }

            
            var departmentDto = new GetDepartmentDTO
            {
                Id = department.Id,
                Name = department.Name
            };

            context.Departments.Remove(department);
            context.SaveChanges();

          
            return Ok(departmentDto);
        }


    }
}
