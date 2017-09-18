using System.Collections.Generic;
using System.Linq;
using GraphDb.API.Domain;
using Neo4j.Driver.V1;
using INode = Neo4j.Driver.V1.INode;

namespace GraphDb.API.Repositories
{
    public class DnsPtrRelationshipRepository : INodeRelationshipRepository<DnsPtrRelationship>
	{
		private readonly IDriver _graphDbDriver;

		public DnsPtrRelationshipRepository(IDriver graphDbDriver)
		{
			this._graphDbDriver = graphDbDriver;
		}

		public IEnumerable<DnsPtrRelationship> All(int skip, int limit)
		{
			using (var session = this._graphDbDriver.Session(AccessMode.Read))
			{
				var statement = "MATCH p=(:IpAddress)-[r:DNS_PTR]->(:Domain) RETURN p SKIP $skip LIMIT $limit";
				var result = session.Run(statement, new Dictionary<string, object>() { { "skip", skip }, { "limit", limit } });

				foreach (var value in result)
				{
					var path = value.Values["p"].As<Neo4j.Driver.V1.IPath>();
					foreach (var relationship in path.Relationships)
					{
						yield return CreateDnsPtrFromRelationship(relationship, path.Nodes);
					}
				}
			}
		}

		public void Save(IEnumerable<DnsPtrRelationship> relationships)
		{
			using (var session = this._graphDbDriver.Session(AccessMode.Write))
			{
				var parameters = new Dictionary<string,object>();
				foreach (var relationship in relationships)
				{
					var statement = $"MATCH (dn:Domain {{name:$name}}), (ip:IpAddress {{ipAddress:$ipAddress}}) CREATE (ip)-[r:{relationship.Type}]->(dn)";
					parameters["name"] = relationship.Domain;
					parameters["ipAddress"] = relationship.IpAddress;
					session.Run(statement, parameters);
				}
			}
		}

		public void Remove(DnsPtrRelationship relationship)
		{
			using (var session = this._graphDbDriver.Session(AccessMode.Write))
			{
				var statement = $"MATCH (:IpAddress {{ipAddress:$ipAddress}})-[r:{relationship.Type}]->(:Domain {{name:$name}}) DELETE r";
				session.Run(statement, new Dictionary<string, object>() {{"name", relationship.Domain}, {"ipAddress", relationship.IpAddress}});
			}
		}

		private static DnsPtrRelationship CreateDnsPtrFromRelationship(IRelationship relationship, IReadOnlyList<INode> nodes)
		{
			var ip = nodes.First(n => n.Id == relationship.StartNodeId);
			var dn = nodes.First(n => n.Id == relationship.EndNodeId);
			return new DnsPtrRelationship()
			{
				Domain = dn.Properties["name"].As<string>(),
				IpAddress = ip.Properties["ipAddress"].As<string>()
			};
		}
	}
}
