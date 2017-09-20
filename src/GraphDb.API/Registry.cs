using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GraphDb.API.Decorators;
using GraphDb.API.Repositories;
using Microsoft.Extensions.Configuration;
using Neo4j.Driver.V1;
using SimpleInjector;

namespace GraphDb.API
{
    public static class Registry
    {
	    public static void Register(Container container)
		{

			container.RegisterSingleton<IConfigurationRoot>(() =>
			{
				var builder = new ConfigurationBuilder()
					.SetBasePath(Directory.GetCurrentDirectory())
					.AddJsonFile("appsettings.json")
					.AddEnvironmentVariables();

				return builder.Build();
			});

			container.RegisterDecorator(typeof(INodeRepository<>), typeof(NodeRepositoryEventDecorator<>));
			container.RegisterDecorator(typeof(INodeRelationshipRepository<>), typeof(NodeRelationshipRepositoryEventDecorator<>));
			container.RegisterDecorator(typeof(IDomainNodeRepository), typeof(DomainNodeRepositoryEventDecorator));
			container.RegisterDecorator(typeof(IIpAddressNodeRepository), typeof(IpAddressNodeRepositoryEventDecorator));

			container.Register(typeof(INodeReadOnlyRepository<>), GetTypesToRegister(typeof(INodeReadOnlyRepository<>),container));
			container.Register(typeof(INodeRelationshipReadOnlyRepository<>), GetTypesToRegister(typeof(INodeRelationshipReadOnlyRepository<>), container));

			container.Register(typeof(INodeRepository<>), GetTypesToRegister(typeof(INodeRepository<>), container));
			container.Register(typeof(INodeRelationshipRepository<>), GetTypesToRegister(typeof(INodeRelationshipRepository<>), container));

			container.Register<IDomainNodeRepository, DomainNodeRepository>();
			container.Register<IIpAddressNodeRepository, IpAddressNodeRepository>();

			container.RegisterSingleton<INodePropertySerialization, Neo4jINodePropertySerialization>();

			container.RegisterSingleton<IEventHub, EventHub>();

			container.Register<IDriver>(() =>
			{
				var config = container.GetInstance<IConfigurationRoot>();
				return GraphDatabase.Driver(config["Neo4jUri"]);
			}, Lifestyle.Scoped);
		}

		private static IEnumerable<Type> GetTypesToRegister(Type serviceType, Container container)
		{
			return container.GetTypesToRegister(serviceType, new[] { typeof(Startup).Assembly }).Where(t => !t.Name.Contains("Decorator"));
		}
	}
}
