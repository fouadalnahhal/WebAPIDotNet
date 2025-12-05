using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIDotNet.DTO;
using WebAPIDotNet.Models;

namespace WebAPIDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private WebAPIDotNetContext _context;

        public EmployeeController(WebAPIDotNetContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public ActionResult<GeneralResponse> GetEmpById(int id)
        {
            var generalResponse = new GeneralResponse();

            var employeeEntity = _context.Employee.Find(id);
            if (employeeEntity == null)
            {
                generalResponse.IsSuccess = false;
                generalResponse.Data = "Id Invalid";
            }
            else
            {
                generalResponse.IsSuccess = true;
                generalResponse.Data = employeeEntity;
            }

            return generalResponse;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetEmpById", new { id = employee.Id }, employee);
        }
    }
}
