namespace GraphDb.API.Domain
{
	public class DomainNode : INode
	{
		public string Name { get; set; }
		public string Type => "Domain";
	}
}