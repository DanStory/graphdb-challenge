using GraphDb.API.Domain;
using GraphDb.API.Repositories;

namespace GraphDb.API.Decorators
{
	public class DomainNodeRepositoryEventDecorator : NodeRepositoryEventDecorator<DomainNode>, IDomainNodeRepository
	{
		public DomainNodeRepositoryEventDecorator(IDomainNodeRepository handler, IEventHub hub) : base(handler, hub)
		{}

		public DomainNode GetByName(string name)
		{
			return ((IDomainNodeRepository) this.Handler).GetByName(name);
		}

		public void RemoveByName(string name)
		{
			((IDomainNodeRepository)this.Handler).RemoveByName(name);
			this.Hub.PublishEvent("Removed", new DomainNode() {Name = name});
		}
	}
}