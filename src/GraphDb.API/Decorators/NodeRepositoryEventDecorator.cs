using System.Linq;
using GraphDb.API.Domain;
using GraphDb.API.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace GraphDb.API.Decorators
{
    public class NodeRepositoryEventDecorator<T> : INodeRepository<T> where T: INode
    {
	    protected readonly INodeRepository<T> Handler;
	    protected readonly IEventHub Hub;

	    public NodeRepositoryEventDecorator(INodeRepository<T> handler, IEventHub hub)
	    {
		    this.Handler = handler;
		    this.Hub = hub;
	    }

		public System.Collections.Generic.IEnumerable<T> All(int skip, int limit)
		{
			return this.Handler.All(skip, limit);
		}

		public void Remove(T node)
		{
			this.Handler.Remove(node);
			this.Hub.PublishEvent("Removed", node);
		}

		public void Save(System.Collections.Generic.IEnumerable<T> nodes)
		{
			var items = nodes as T[] ?? nodes.ToArray();
			this.Handler.Save(items);
			this.Hub.PublishEvent("Added", items);
		}
	}
}
