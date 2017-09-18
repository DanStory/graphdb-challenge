using System;
using System.Collections.Generic;
using System.Net;
using GraphDb.API.Domain;
using GraphDb.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GraphDb.API.Controllers
{
    [Route("v1/Relationships/DNS_CHILDs")]
    public class DnsChildrenController : Controller
    {
	    private readonly INodeRelationshipRepository<DnsChildRelationship> _repository;

	    public DnsChildrenController(INodeRelationshipRepository<DnsChildRelationship> repository)
	    {
		    this._repository = repository;
	    }

        [HttpGet]
        public IEnumerable<DnsChildRelationship> Get([FromQuery]int skip=0, [FromQuery]int limit=Int32.MaxValue)
        {
	        return this._repository.All(skip, limit);
        }

        [HttpPost]
        public void Post([FromBody]DnsChildRelationship[] relationships)
        {
			this._repository.Save(relationships);
        }
		
        [HttpDelete]
        public void Post([FromBody]DnsChildRelationship relationship)
        {
	        this._repository.Remove(relationship);
        }
    }
}
