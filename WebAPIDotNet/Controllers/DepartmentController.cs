using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebAPIDotNet.DTO;
using WebAPIDotNet.Models;

namespace WebAPIDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        WebAPIDotNetContext _context;

        public DepartmentController(WebAPIDotNetContext context)
        {
            _context = context;
        }

        [HttpGet("p")]
        public ActionResult<List<DeptWithEmpCountDTO>> GetDeptDetails()
        {
            var deptlist =
                _context.Department.Include(d => d.Employees).ToList();


            var deptListDto = deptlist.Select(x => new DeptWithEmpCountDTO
            {
                ID = x.Id,
                Name = x.Name,
                EmpCount = x.Employees.Count()
            }).ToList();

            return deptListDto;
            //return Ok(deptlistDto); IActionREsult
        }

        [HttpGet]
        public IActionResult DispalyAllDepartment()
        {
            var departments = _context.Department.ToList();
            return Ok(departments);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetDeptById(int id)
        {
            var department = _context.Department.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        [HttpGet("{name:alpha}")]
        public IActionResult GetDeptByName(string name)
        {
            var department = _context.Department.FirstOrDefault(x => x.Name == name);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> AddDept(Department department)
        {
            _context.Department.Add(department);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetDeptById", new { id = department.Id }, department);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateDept(int id, Department department)
        {
            var departmentEntity = _context.Department.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            departmentEntity.Name = department.Name;
            departmentEntity.ManagerName = department.ManagerName;
            _context.SaveChanges();
            return NoContent();
        }
    }
}
