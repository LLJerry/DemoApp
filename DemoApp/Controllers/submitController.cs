using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace DemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class submitController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(JObject InputData)
        {
            JObject Responce = SupervisorProc.ProcessUpdate(InputData);

            return Ok(Responce);
        }
    }
}
