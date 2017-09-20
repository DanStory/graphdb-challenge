using System;
using System.Net;
using GraphDb.API.Domain;
using GraphDb.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GraphDb.API.Controllers
{
    [Route("v1/[controller]")]
    public class StatusController : Controller
    {
	    private readonly INodeReadOnlyRepository<INode> _repository;

	    public StatusController(IConfigurationRoot config, INodeReadOnlyRepository<INode> repository)
	    {
		    this._repository = repository;
	    }

		/// <summary>
		/// Get Status of GraphDb.API
		/// </summary>
		/// <returns></returns>
        [HttpGet] [HttpHead]
        public Status Get()
        {
	        try
	        {
		        var nodes = this._repository.All(0, 1);
	        }
	        catch (Exception ex)
	        {
		        this.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
				return new Status { State = "Offline", Error = ex.Message};
	        }
	        

	        return new Status { State = "Online"};
        }
    }
}
