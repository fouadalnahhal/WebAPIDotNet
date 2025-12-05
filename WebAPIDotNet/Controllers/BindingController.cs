using Microsoft.AspNetCore.Mvc;
using WebAPIDotNet.Models;

namespace WebAPIDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BindingController : ControllerBase
    {
        [HttpGet("{name:alpha}/{age:int}")]
        public IActionResult TestPrimitive(int age, string name)
        {
            return Ok();
        }

        [HttpPost("{name:alpha}")]
        public IActionResult TestObj(Department department, string name)
        {
            return Ok();
        }

        [HttpGet("{Id:int}/{Name:alpha}/{ManagerName:alpha}")]
        //public IActionResult TestCustomBind(int id, string name, string managerName)
        public IActionResult TestCustomBind([FromRoute] Department department)
        {
            return Ok();
        }
    }
}
