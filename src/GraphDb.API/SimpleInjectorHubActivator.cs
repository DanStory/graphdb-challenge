using Microsoft.AspNetCore.SignalR;
using SimpleInjector;

namespace GraphDb.API
{
	public partial class Startup
    {
		private class SimpleInjectorHubActivator<TService,T> : IHubActivator<T> where T: Hub where TService: class
		{
			private readonly Container _container;

			public SimpleInjectorHubActivator(Container container)
			{
				this._container = container;
			}

			public T Create()
			{
				return this._container.GetInstance<TService>() as T;
			}

			public void Release(T hub)
			{
				
			}
		}

	}
}
