using System;
using System.Collections.Generic;
using System.Net;
using GraphDb.API.Domain;
using GraphDb.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GraphDb.API.Controllers
{
    [Route("v1/Nodes/[controller]")]
    public class DomainsController : Controller
    {
	    private readonly IDomainNodeRepository _repository;

	    public DomainsController(IDomainNodeRepository repository)
	    {
		    this._repository = repository;
	    }

        [HttpGet]
        public IEnumerable<DomainNode> Get([FromQuery]int skip=0, [FromQuery]int limit=Int32.MaxValue)
        {
	        return this._repository.All(skip, limit);
        }


        [HttpGet("{name}")]
        public DomainNode Get(string name)
        {
	        var node = this._repository.GetByName(name);

	        if (node == null)
	        {
		        this.Response.StatusCode = (int) HttpStatusCode.NotFound;
	        }

	        return node;
        }
		
        [HttpPost]
        public void Post([FromBody]DomainNode[] nodes)
        {
			this._repository.Save(nodes);
        }
		
        [HttpDelete("{name}")]
        public void Delete(string name)
        {
	        this._repository.RemoveByName(name);
        }
    }
}
