using Microsoft.AspNetCore.Mvc;

namespace ServiceC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCController : ControllerBase
    {
        [HttpGet]
        [ActionName("ServiceC")]
        public string GetServiceName()
        {
            return "ServiceC";
        }
    }
}
