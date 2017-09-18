using System;
using System.Collections.Generic;
using System.Net;
using GraphDb.API.Domain;
using GraphDb.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GraphDb.API.Controllers
{
    [Route("v1/Nodes/[controller]")]
    public class IpAddressesController : Controller
    {
	    private readonly IIpAddressNodeRepository _repository;

	    public IpAddressesController(IIpAddressNodeRepository repository)
	    {
		    this._repository = repository;
	    }

        [HttpGet]
        public IEnumerable<IpAddressNode> Get([FromQuery]int skip=0, [FromQuery]int limit=Int32.MaxValue)
        {
	        return this._repository.All(skip, limit);
        }

	    [HttpGet("{ipAddress}")]
	    public IpAddressNode Get(string ipAddress)
	    {
		    var node = this._repository.GetByIpAddress(ipAddress);

		    if (node == null)
		    {
			    this.Response.StatusCode = (int)HttpStatusCode.NotFound;
		    }

		    return node;
	    }

	    [HttpPost]
	    public void Post([FromBody]IpAddressNode[] nodes)
	    {
		    this._repository.Save(nodes);
	    }

	    [HttpDelete("{ipAddress}")]
	    public void Delete(string ipAddress)
	    {
		    this._repository.RemoveByIpAddress(ipAddress);
	    }
	}
}
