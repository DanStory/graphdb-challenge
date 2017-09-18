using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GraphDb.API.Domain;
using Neo4j.Driver.V1;

namespace GraphDb.API.Repositories
{
	public interface IDomainNodeRepository : INodeRepository<DomainNode>
	{
		DomainNode GetByName(string name);
		void RemoveByName(string name);
	}

	public class DomainNodeRepository : IDomainNodeRepository
	{
		private readonly IDriver _graphDbDriver;

		public DomainNodeRepository(IDriver graphDbDriver)
		{
			this._graphDbDriver = graphDbDriver;
		}

		public IEnumerable<DomainNode> All(int skip, int limit)
		{
			using (var session = this._graphDbDriver.Session(AccessMode.Read))
			{
				var statement = "MATCH (dn:Domain) RETURN dn SKIP $skip LIMIT $limit";
				var result = session.Run(statement, new Dictionary<string, object>() { { "skip", skip }, { "limit", limit } });

				return result.Select(record => CreateDomainFromNode(record.Values["dn"].As<Neo4j.Driver.V1.INode>()));
			}
		}

		public DomainNode GetByName(string name)
		{
			using (var session = this._graphDbDriver.Session(AccessMode.Read))
			{
				var statement = "MATCH (dn:Domain {name:$name}) RETURN dn LIMIT 1";
				var result = session.Run(statement, new Dictionary<string, object>() { { "name", name } });

				return result.Select(record => CreateDomainFromNode(record.Values["dn"].As<Neo4j.Driver.V1.INode>())).FirstOrDefault();
			}
		}

		public void Save(IEnumerable<DomainNode> nodes)
		{
			var parameters = new Dictionary<string,object>();
			var statement = new StringBuilder();
			statement.Append("CREATE ");

			var index = 0;
			foreach (var node in nodes)
			{
				if (index > 0)
				{
					statement.Append(", ");
				}
				statement.Append($"(dn{index}:Domain {{name:$name{index}}}");
				parameters.Add($"name{index}", node.Name);
				statement.Append(")");

				index++;
			}

			using (var session = this._graphDbDriver.Session(AccessMode.Write))
			{
				session.Run(statement.ToString(), parameters);
			}
		}

		public void Remove(DomainNode node)
		{
			this.RemoveByName(node.Name);
		}

		public void RemoveByName(string name)
		{
			using (var session = this._graphDbDriver.Session(AccessMode.Write))
			{
				var statement = "MATCH (dn:Domain {name:$name}) DETACH DELETE dn";
				session.Run(statement, new Dictionary<string, object>() { { "name", name } });
			}
		}

		private static DomainNode CreateDomainFromNode(Neo4j.Driver.V1.INode node)
		{
			return new DomainNode()
			{
				Name = node.Properties["name"].As<string>()
			};
		}
	}
}
