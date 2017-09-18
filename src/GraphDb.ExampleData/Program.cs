using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Swagger;
using Swagger.Models;

namespace GraphDb.ExampleData
{
    class Program
    {
		static readonly Random Random = new Random((int)(DateTime.UtcNow.Ticks % int.MaxValue));
	    private static readonly GraphDbAPI Client = new GraphDbAPI(new Uri("http://localhost:8080/"));

        static void Main(string[] args)
        {
			Console.WriteLine("Press any key to continue...");
	        Console.Read();

			Console.WriteLine($"Generating sample data...");
	        var exampleData = Generate(1000);

	        var batchSize = 100;

			Console.WriteLine("\tAdding domains");
		    PostBatch(Client.V1NodesDomainsPost, exampleData.Domains, batchSize);

		    Console.WriteLine("\tAdding ip addresses");
			PostBatch(Client.V1NodesIpAddressesPost, exampleData.IpAddresses, batchSize);


		    var relationshipDnsClient = new V1RelationshipsDNS(Client);

		    Console.WriteLine("\tAdding ip address -> domain relationships");
			PostBatch(relationshipDnsClient.PTRsPost, exampleData.DnsPtrs, batchSize);

		    Console.WriteLine("\tAdding domain -> domain relationships");
			PostBatch(relationshipDnsClient.CHILDsPost, exampleData.DnsChild, batchSize);

			Console.WriteLine("Done generating sample data");
			Console.WriteLine();


			// TODO: Continue to stream node updates
			/*
			Console.WriteLine("Streaming random updates, press any key to quit.");
			var cancellation = new CancellationTokenSource();
	        var task = Task.Factory.StartNew(() => RandomUpdates(cancellation.Token), cancellation.Token);
			task.Start();
	        Console.Read();
	        cancellation.Cancel();
	        task.Wait();
			*/
		}

		static void PostBatch<T>(Action<IList<T>> post, IEnumerable<T> items, int batchSize)
	    {
			var batch = new List<T>(batchSize);
		    foreach (var item in items)
		    {
			    batch.Add(item);
			    if (batch.Count >= batchSize)
			    {
				    post(batch);
				    batch.Clear();
			    }
		    }
		    if (batch.Count > 0)
		    {
				post(batch);
				batch.Clear();
			}
		}

	    static void RandomUpdates(CancellationToken cancelToken)
	    {
		    while (cancelToken.IsCancellationRequested == false)
		    {
			    
		    }
	    }

	    static (IEnumerable<DomainNode> Domains, IEnumerable<IpAddressNode> IpAddresses, IEnumerable<DnsPtrRelationship> DnsPtrs, IEnumerable<DnsChildRelationship> DnsChild) Generate(int count)
	    {
		    var domains = new HashSet<string>();
		    var ips = new HashSet<string>();
			var dnsptrs = new List<DnsPtrRelationship>();
		    var dnschilds = new List<DnsChildRelationship>();

			while (domains.Count < count)
		    {
			    string domain;
			    if (domains.Count == 0 || Random.Next(3) > 0) // two-thirds chance
			    {
				    // generate random domain
				    domain = GenerateRandomDomain((v) => domains.Contains(v));
			    }
			    else
			    {
				    // generate random child domain
				    var index = Random.Next(domains.Count);
				    var parent = domains.ElementAt(index);
				    domain = GenerateRandomDomain((v) => domains.Contains(v), parent);

				    dnschilds.Add(new DnsChildRelationship() {ParentDomain = parent, ChildDomain = domain});
			    }

			    domains.Add(domain);


			    string ipAddress;
			    if (ips.Count == 0 || Random.Next(3) > 0) // two-thirds chance
			    {
				    // generate random ip address
				    ipAddress = GenerateRandomIpAddress((v) => ips.Contains(v));
			    }
			    else
			    {
				    // use existing ip address
				    var index = Random.Next(ips.Count);
				    ipAddress = ips.ElementAt(index);
			    }

			    ips.Add(ipAddress);
			    dnsptrs.Add(new DnsPtrRelationship() {Domain = domain, IpAddress = ipAddress});
		    }

		    return (domains.Select(dn => new DomainNode() { Name = dn }), ips.Select(ip => new IpAddressNode() { IpAddress = ip }), dnsptrs, dnschilds);
	    }

	    static string GenerateRandomDomain(Func<string, bool> exists, string parent = null)
	    {
		    var domain = parent;
		    int index;
		    if (domain == null)
		    {
			    index = Random.Next(SampleData.TopLevelDomains.Length);
			    domain = SampleData.TopLevelDomains[index];
		    }

		    string value;
		    do
		    {
			    value = String.Empty;
			    var count = Random.Next(3);
			    for (var i = 0; i < count; i++)
			    {
				    switch (Random.Next(3))
				    {
					    case 0:
						    index = Random.Next(SampleData.Numbers.Length);
						    value += DomainSafe(SampleData.Numbers[index]);
						    break;
					    case 1:
						    index = Random.Next(SampleData.DogBreeds.Length);
						    value += DomainSafe(SampleData.DogBreeds[index]);
						    break;
					    case 2:
						    index = Random.Next(SampleData.UnitedStateCities.Length);
						    value += DomainSafe(SampleData.UnitedStateCities[index]);
						    break;
				    }
			    }
		    } while (exists($"{value}.{domain}"));

			domain = $"{value}.{domain}";

			return domain;
	    }

	    static string GenerateRandomIpAddress(Func<string, bool> exists)
	    {
		    string ipAddress;
		    do
		    {
			    ipAddress = $"{Random.Next(1, 255)}.{Random.Next(0, 255)}.{Random.Next(0, 255)}.{Random.Next(0, 255)}";
		    } while (exists(ipAddress));

		    return ipAddress;
	    }

		static string DomainSafe(string value)
	    {
		    return value.Replace(" ", "");
	    }

	}
}
