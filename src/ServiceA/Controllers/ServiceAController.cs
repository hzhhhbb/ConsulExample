using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceAController : ControllerBase
    {
        [HttpGet]
        [ActionName("ServiceName")]
        public string GetServiceName()
        {
            return "ServiceA";
        }
    }
}
