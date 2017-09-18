using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GraphDb.API.Controllers
{
    [Route("v1/[controller]")]
    public class StatusController : Controller
    {
	    private readonly IConfigurationRoot _config;

	    public StatusController(IConfigurationRoot config)
	    {
		    this._config = config;
	    }

        [HttpGet] [HttpHead]
        public object Get()
        {
	        return new {State = "Healthy"};
        }
    }
}
