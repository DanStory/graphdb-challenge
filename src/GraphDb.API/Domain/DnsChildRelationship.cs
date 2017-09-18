namespace GraphDb.API.Domain
{
	public class DnsChildRelationship : INodeRelationship
	{
		public string ChildDomain { get; set; }
		public string ParentDomain { get; set; }
		public string Type => "DNS_CHILD";
	}
}