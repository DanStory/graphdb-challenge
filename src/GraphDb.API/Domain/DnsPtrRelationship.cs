namespace GraphDb.API.Domain
{
	public class DnsPtrRelationship : INodeRelationship
	{
		public string IpAddress { get; set; }
		public string Domain { get; set; }
		public string Type => "DNS_PTR";
	}
}