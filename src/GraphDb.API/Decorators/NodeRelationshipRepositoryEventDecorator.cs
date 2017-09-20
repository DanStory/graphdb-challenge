using System.Linq;
using GraphDb.API.Domain;
using GraphDb.API.Repositories;

namespace GraphDb.API.Decorators
{
	public class NodeRelationshipRepositoryEventDecorator<T> : INodeRelationshipRepository<T> where T : INodeRelationship
	{
		protected readonly INodeRelationshipRepository<T> Handler;
		protected readonly IEventHub Hub;

		public NodeRelationshipRepositoryEventDecorator(INodeRelationshipRepository<T> handler, IEventHub hub)
		{
			this.Handler = handler;
			this.Hub = hub;
		}

		public System.Collections.Generic.IEnumerable<T> All(int skip, int limit)
		{
			return this.Handler.All(skip, limit);
		}

		public void Remove(T relationship)
		{
			this.Handler.Remove(relationship);
			this.Hub.PublishEvent("Removed", relationship);
		}

		public void Save(System.Collections.Generic.IEnumerable<T> relationships)
		{
			var items = relationships as T[] ?? relationships.ToArray();
			this.Handler.Save(items);
			this.Hub.PublishEvent("Added", items);
		}
	}
}