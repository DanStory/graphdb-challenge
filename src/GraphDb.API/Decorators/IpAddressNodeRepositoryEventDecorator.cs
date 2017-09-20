using GraphDb.API.Domain;
using GraphDb.API.Repositories;

namespace GraphDb.API.Decorators
{
	public class IpAddressNodeRepositoryEventDecorator : NodeRepositoryEventDecorator<IpAddressNode>, IIpAddressNodeRepository
	{
		public IpAddressNodeRepositoryEventDecorator(IIpAddressNodeRepository handler, IEventHub hub) : base(handler, hub)
		{ }

		public IpAddressNode GetByIpAddress(string ipAddress)
		{
			return ((IIpAddressNodeRepository)this.Handler).GetByIpAddress(ipAddress);
		}

		public void RemoveByIpAddress(string ipAddress)
		{
			((IIpAddressNodeRepository)this.Handler).RemoveByIpAddress(ipAddress);
			this.Hub.PublishEvent("Removed", new IpAddressNode() { IpAddress = ipAddress });
		}
	}
}