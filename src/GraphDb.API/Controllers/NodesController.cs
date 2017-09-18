using System;
using System.Collections.Generic;
using GraphDb.API.Domain;
using GraphDb.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GraphDb.API.Controllers
{
    [Route("v1/[controller]")]
    public class NodesController : Controller
    {
	    private readonly INodeReadOnlyRepository<INode> _repository;

	    public NodesController(INodeReadOnlyRepository<INode> repository)
	    {
		    this._repository = repository;
	    }

        [HttpGet]
        public IEnumerable<INode> Get([FromQuery]int skip=0, [FromQuery]int limit=Int32.MaxValue)
        {
	        return this._repository.All(skip, limit);
        }
    }
}
