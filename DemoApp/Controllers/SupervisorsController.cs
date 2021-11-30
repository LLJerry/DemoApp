using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupervisorsController : ControllerBase
    {
        [HttpGet]
        public IActionResult get()
        {

            String outputString = SupervisorProc.ProcessGet();
            return Ok(outputString);
        }
                
    }
}
