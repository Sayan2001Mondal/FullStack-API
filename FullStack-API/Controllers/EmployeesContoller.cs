using FullStack_API.Data;
using FullStack_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FullStack_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly FullStackDbContext _fullStackDbContext;
        public EmployeesController(FullStackDbContext fullStackDbContext) 
        {
            _fullStackDbContext = fullStackDbContext;
        }
        [HttpGet]
        [Route(nameof(GetAllEmployees))]
        public async Task<IActionResult> GetAllEmployees()
        {
          var employees = await  _fullStackDbContext.Employees.AsNoTracking().ToListAsync();
          return Ok(employees);
        }
        [HttpPost]
        [Route(nameof(AddEmployee))]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.Id = Guid.NewGuid();
                _ = await _fullStackDbContext.Employees.AddAsync(employee);
                _ = await _fullStackDbContext.SaveChangesAsync();
                var newemp = await _fullStackDbContext.Employees.AsNoTracking().FirstOrDefaultAsync((x) => x.Id == employee.Id);
                return Created($"/GetEmployee/{newemp!.Id}",newemp);
            }
            return BadRequest("Model is not Valid");
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute]Guid id)
        {
          var employee = await _fullStackDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if(employee is null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployee)
        {
            var employee = await _fullStackDbContext.Employees.FindAsync(id);
            if(employee is null)
            {
                return NotFound();
            }
            employee.Name = updateEmployee.Name;
            employee.Email = updateEmployee.Email;
            employee.Phone = updateEmployee.Phone;
            employee.Salary = updateEmployee.Salary;
            employee.Department = updateEmployee.Department;

            await _fullStackDbContext.SaveChangesAsync();
            return Ok(employee);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _fullStackDbContext.Employees.FindAsync(id);
            if(employee is null)
            {
                return NotFound();
            }
            _fullStackDbContext.Employees.Remove(employee);
            await _fullStackDbContext.SaveChangesAsync();
            return Ok(employee);
        }
    }
        

}
