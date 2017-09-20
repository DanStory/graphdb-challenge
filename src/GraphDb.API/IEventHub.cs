namespace GraphDb.API
{
	public interface IEventHub
	{
		void PublishEvent(string @event, object item);
	}
}