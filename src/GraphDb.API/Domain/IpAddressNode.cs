namespace GraphDb.API.Domain
{
	public class IpAddressNode : INode
	{
		public string IpAddress { get; set; }

		public string Type => "IpAddress";
	}
}