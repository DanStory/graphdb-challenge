using System.IO;
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

		    container.Register(typeof(INodeReadOnlyRepository<>), new[] { typeof(Startup).Assembly });
		    container.Register(typeof(INodeRelationshipReadOnlyRepository<>), new[] { typeof(Startup).Assembly });

			container.Register(typeof(INodeRepository<>), new[] {typeof(Startup).Assembly});
		    container.Register(typeof(INodeRelationshipRepository<>), new[] { typeof(Startup).Assembly });

			container.Register<IDomainNodeRepository, DomainNodeRepository>();
		    container.Register<IIpAddressNodeRepository, IpAddressNodeRepository>();

			container.RegisterSingleton<INodePropertySerialization, Neo4jINodePropertySerialization>();

		    container.Register<IDriver>(() =>
		    {
			    var config = container.GetInstance<IConfigurationRoot>();
			    return GraphDatabase.Driver(config["Neo4jUri"]);
		    }, Lifestyle.Scoped);
	    }
    }
}
