using System.IO;
using GraphDb.API.Domain;

namespace GraphDb.API
{
	public interface INodePropertySerialization
	{
		string Serialize(INode node);
		void Serialize(TextWriter writer, INode node);
	}
}