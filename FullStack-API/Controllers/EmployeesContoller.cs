using FullStack_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStack_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesContoller : Controller
    {
        private readonly FullStackDbContext _fullStackDbContext;
        public EmployeesContoller(FullStackDbContext fullStackDbContext) 
        {
            _fullStackDbContext = fullStackDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
          var employees = await  _fullStackDbContext.Employees.ToListAsync();
            return Ok(employees);
        }
    }
}
