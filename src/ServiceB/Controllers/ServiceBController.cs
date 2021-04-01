using Microsoft.AspNetCore.Mvc;

namespace ServiceB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceBController : ControllerBase
    {
        [HttpGet]
        [ActionName("ServiceB")]
        public string GetServiceName()
        {
            return "ServiceB";
        }
    }
}
