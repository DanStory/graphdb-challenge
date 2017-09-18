using System;
using System.Collections.Generic;
using System.Linq;
using GraphDb.API.Domain;
using Neo4j.Driver.V1;
using INode = GraphDb.API.Domain.INode;

namespace GraphDb.API.Repositories
{
	public class NodeRepository : INodeReadOnlyRepository<INode>
	{
		private readonly IDriver _graphDbDriver;

		public NodeRepository(IDriver graphDbDriver)
		{
			this._graphDbDriver = graphDbDriver;
		}

		public IEnumerable<INode> All(int skip, int limit)
		{
			using (var session = this._graphDbDriver.Session(AccessMode.Read))
			{
				var statement = "MATCH (n) RETURN n SKIP $skip LIMIT $limit";
				var result = session.Run(statement, new Dictionary<string, object>() { { "skip", skip }, { "limit", limit } });

				return result.Select(record => CreateNode(record.Values["n"].As<Neo4j.Driver.V1.INode>()));
			}
		}

		private static INode CreateNode(Neo4j.Driver.V1.INode node)
		{
			// TODO: Refactor to open/close principle

			if (node.Labels.Contains("Domain"))
			{
				return new DomainNode()
				{
					Name = node.Properties["name"].As<string>()
				};
			}
			if (node.Labels.Contains("IpAddress"))
			{
				return new IpAddressNode()
				{
					IpAddress = node.Properties["ipAddress"].As<string>()
				};
			}

			throw new NotSupportedException($"Node not supported (labels:{String.Join(",", node.Labels)})");
		}
	}
}
