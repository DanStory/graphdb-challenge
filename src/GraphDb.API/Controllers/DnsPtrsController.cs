using System;
using System.Collections.Generic;
using System.Net;
using GraphDb.API.Domain;
using GraphDb.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GraphDb.API.Controllers
{
    [Route("v1/Relationships/DNS_PTRs")]
    public class DnsPtrsController : Controller
    {
	    private readonly INodeRelationshipRepository<DnsPtrRelationship> _repository;

	    public DnsPtrsController(INodeRelationshipRepository<DnsPtrRelationship> repository)
	    {
		    this._repository = repository;
	    }

        [HttpGet]
        public IEnumerable<DnsPtrRelationship> Get([FromQuery]int skip=0, [FromQuery]int limit=Int32.MaxValue)
        {
	        return this._repository.All(skip, limit);
        }

        [HttpPost]
        public void Post([FromBody]DnsPtrRelationship[] relationships)
        {
			this._repository.Save(relationships);
        }
		
        [HttpDelete]
        public void Post([FromBody]DnsPtrRelationship relationship)
        {
	        this._repository.Remove(relationship);
        }
    }
}
