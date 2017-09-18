using System.Collections.Generic;
using System.Linq;
using GraphDb.API.Domain;
using Neo4j.Driver.V1;
using INode = Neo4j.Driver.V1.INode;

namespace GraphDb.API.Repositories
{
    public class DnsChildRelationshipRepository : INodeRelationshipRepository<DnsChildRelationship>
	{
		private readonly IDriver _graphDbDriver;

		public DnsChildRelationshipRepository(IDriver graphDbDriver)
		{
			this._graphDbDriver = graphDbDriver;
		}

		public IEnumerable<DnsChildRelationship> All(int skip, int limit)
		{
			using (var session = this._graphDbDriver.Session(AccessMode.Read))
			{
				var statement = "MATCH p=(:Domain)-[r:DNS_CHILD]->(:Domain) RETURN p SKIP $skip LIMIT $limit";
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

		public void Save(IEnumerable<DnsChildRelationship> relationships)
		{
			using (var session = this._graphDbDriver.Session(AccessMode.Write))
			{
				var parameters = new Dictionary<string,object>();
				foreach (var relationship in relationships)
				{
					var statement = $"MATCH (pdn:Domain {{name:$parent}}), (cdn:Domain {{name:$child}}) CREATE (cdn)-[r:{relationship.Type}]->(pdn)";
					parameters["parent"] = relationship.ParentDomain;
					parameters["child"] = relationship.ChildDomain;
					session.Run(statement, parameters);
				}
			}
		}

		public void Remove(DnsChildRelationship relationship)
		{
			using (var session = this._graphDbDriver.Session(AccessMode.Write))
			{
				var statement = $"MATCH (:Domain {{name:$child}})-[r:{relationship.Type}]->(:Domain {{name:$parent}}) DELETE r";
				session.Run(statement, new Dictionary<string, object>() {{"parent", relationship.ParentDomain }, { "child", relationship.ChildDomain}});
			}
		}

		private static DnsChildRelationship CreateDnsPtrFromRelationship(IRelationship relationship, IReadOnlyList<INode> nodes)
		{
			var cdn = nodes.First(n => n.Id == relationship.StartNodeId);
			var pdn = nodes.First(n => n.Id == relationship.EndNodeId);
			return new DnsChildRelationship()
			{
				ParentDomain = pdn.Properties["name"].As<string>(),
				ChildDomain = cdn.Properties["name"].As<string>()
			};
		}
	}
}
