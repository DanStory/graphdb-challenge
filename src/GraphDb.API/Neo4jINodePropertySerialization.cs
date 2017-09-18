using System.IO;
using System.Text;
using GraphDb.API.Domain;
using Newtonsoft.Json;

namespace GraphDb.API
{
	public class Neo4jINodePropertySerialization : INodePropertySerialization
	{
		private readonly JsonSerializer _serializer = new JsonSerializer(){Formatting = Formatting.None};

		public void Serialize(TextWriter writer, INode node)
		{
			this._serializer.Serialize(writer, node);
		}

		public string Serialize(INode node)
		{
			var serializedNode = new StringBuilder();
			using (var writer = new StringWriter(serializedNode))
			{
				this._serializer.Serialize(writer, node);
			}
			return serializedNode.ToString();
		}
	}
}
