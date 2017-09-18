using System;
using System.Collections.Generic;
using System.Linq;
using GraphDb.API.Domain;
using Neo4j.Driver.V1;

namespace GraphDb.API.Repositories
{
	public class NodeRelationshipRepository : INodeRelationshipReadOnlyRepository<INodeRelationship>
	{
		private readonly IDriver _graphDbDriver;

		public NodeRelationshipRepository(IDriver graphDbDriver)
		{
			this._graphDbDriver = graphDbDriver;
		}

		public IEnumerable<INodeRelationship> All(int skip, int limit)
		{
			using (var session = this._graphDbDriver.Session(AccessMode.Read))
			{
				var statement = "MATCH p=()-->() RETURN p SKIP $skip LIMIT $limit";
				var result = session.Run(statement, new Dictionary<string, object>() { { "skip", skip }, { "limit", limit } });

				foreach (var value in result)
				{
					var path = value.Values["p"].As<Neo4j.Driver.V1.IPath>();
					foreach (var relationship in path.Relationships)
					{
						yield return CreateNodeRelationship(relationship, path.Nodes);
					}
				}
			}
		}

		private static INodeRelationship CreateNodeRelationship(IRelationship relationship, IReadOnlyList<Neo4j.Driver.V1.INode> nodes)
		{
			var source = nodes.First(n => n.Id == relationship.StartNodeId);
			var target = nodes.First(n => n.Id == relationship.EndNodeId);

			// TODO: Refactor to open/close principle

			if (relationship.Type =="DNS_PTR")
			{
				return new DnsPtrRelationship()
				{
					IpAddress = source.Properties["ipAddress"].As<string>(),
					Domain = target.Properties["name"].As<string>()
				};
			}
			if (relationship.Type == "DNS_CHILD")
			{
				return new DnsChildRelationship()
				{
					ChildDomain = source.Properties["name"].As<string>(),
					ParentDomain = target.Properties["name"].As<string>()
				};
			}

			throw new NotSupportedException($"Relationship not supported (type:{relationship.Type})");
		}
	}
}
