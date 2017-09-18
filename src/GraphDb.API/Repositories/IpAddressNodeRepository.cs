using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphDb.API.Domain;
using Neo4j.Driver.V1;

namespace GraphDb.API.Repositories
{
	public interface IIpAddressNodeRepository : INodeRepository<IpAddressNode>
	{
		IpAddressNode GetByIpAddress(string ipAddress);
		void RemoveByIpAddress(string ipAddress);
	}

	public class IpAddressNodeRepository : IIpAddressNodeRepository
	{
		private readonly IDriver _graphDbDriver;

		public IpAddressNodeRepository(IDriver graphDbDriver)
		{
			this._graphDbDriver = graphDbDriver;
		}

		public IEnumerable<IpAddressNode> All(int skip, int limit)
		{
			using (var session = this._graphDbDriver.Session(AccessMode.Read))
			{
				var statement = "MATCH (ip:IpAddress) RETURN ip SKIP $skip LIMIT $limit";
				var result = session.Run(statement, new Dictionary<string, object>() { { "skip", skip }, { "limit", limit } });

				return result.Select(record => CreateIpAddressFromNode(record.Values["ip"].As<Neo4j.Driver.V1.INode>()));
			}
		}

		public IpAddressNode GetByIpAddress(string ipAddress)
		{
			using (var session = this._graphDbDriver.Session(AccessMode.Read))
			{
				var statement = "MATCH (ip:IpAddress {name:$name}) RETURN ip LIMIT 1";
				var result = session.Run(statement, new Dictionary<string, object>() { { "ipAddress", ipAddress } });

				return result.Select(record => CreateIpAddressFromNode(record.Values["ip"].As<Neo4j.Driver.V1.INode>())).FirstOrDefault();
			}
		}

		public void Save(IEnumerable<IpAddressNode> nodes)
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
				statement.Append($"(ip{index}:IpAddress {{ipAddress:$ipAddress{index}}}");
				parameters.Add($"ipAddress{index}", node.IpAddress);
				statement.Append(")");

				index++;
			}

			using (var session = this._graphDbDriver.Session(AccessMode.Write))
			{
				session.Run(statement.ToString(), parameters);
			}
		}

		public void Remove(IpAddressNode node)
		{
			this.RemoveByIpAddress(node.IpAddress);
		}

		public void RemoveByIpAddress(string ipAddress)
		{
			using (var session = this._graphDbDriver.Session(AccessMode.Write))
			{
				var statement = "MATCH (ip:IpAddress {ipAddress:$ipAddress}) DETACH DELETE ip";
				session.Run(statement, new Dictionary<string, object>() {{"ipAddress", ipAddress}});
			}
		}

		private static IpAddressNode CreateIpAddressFromNode(Neo4j.Driver.V1.INode node)
		{
			return new IpAddressNode()
			{
				IpAddress = node.Properties["ipAddress"].As<string>()
			};
		}
	}
}
