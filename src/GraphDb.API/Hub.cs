using Microsoft.AspNetCore.SignalR;

namespace GraphDb.API
{
    public class EventHub : Hub, IEventHub
	{
		public void PublishEvent(string @event, object item)
		{
			Clients?.All.InvokeAsync($"On{@event}", new[] {item});
		}
    }
}
