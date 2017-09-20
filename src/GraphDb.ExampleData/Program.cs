using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Swagger;
using Swagger.Models;

namespace GraphDb.ExampleData
{
	class Program
	{
		static readonly Random Random = new Random((int) (DateTime.UtcNow.Ticks % int.MaxValue));
		private static readonly GraphDbAPI Client = new GraphDbAPI(new Uri(Environment.GetEnvironmentVariable("APIURL")));

		static void Main(string[] args)
		{
			Console.WriteLine("Welcome to GraphDb.ExampleData Console App");


			Console.WriteLine("\tWaiting for GraphDb.API");
			var timestamp = DateTime.UtcNow;
			var apiIsUp = false;
			while ((DateTime.UtcNow - timestamp).TotalMinutes < 3)
			{
				try
				{
					Client.V1StatusHead();
					apiIsUp = true;
					break;
				}
				catch
				{
				}
			}
			if (!apiIsUp)
			{
				Console.WriteLine();
				Console.WriteLine("GraphDb.API did not come up in a timely manner");
				return;
			}

			Console.WriteLine();
			Console.WriteLine("Ready to generate sample data, press any key to continue...");
			Console.Read();

			Console.WriteLine($"Generating sample data...");
			var exampleData = Generate(1000);

			var batchSize = 100;

			Console.WriteLine("\tAdding domains");
			PostBatch(Client.V1NodesDomainsPost, exampleData.Domains, batchSize);

			Console.WriteLine("\tAdding ip addresses");
			PostBatch(Client.V1NodesIpAddressesPost, exampleData.IpAddresses, batchSize);


			Console.WriteLine("\tAdding ip address -> domain relationships");
			PostBatch(Client.V1RelationshipsDnsPtrsPost, exampleData.DnsPtrs, batchSize);

			Console.WriteLine("\tAdding domain -> domain relationships");
			PostBatch(Client.V1RelationshipsDnsChildrenPost, exampleData.DnsChild, batchSize);

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
				var action = Random.Next(3);

				switch (action)
				{
					case 0: //Add Domain
						var domain = GenerateRandomDomain((dn) =>
						{
							try
							{
								Client.V1NodesDomainsByNameGet(dn);
								return true;
							}
							catch
							{
								return false;
							}
						});

						Client.V1NodesDomainsPost(new[] {new DomainNode(name: domain),});
						break;

					case 1: //Delete Domain

						break;

					case 2: //Add PTR
						break;

					case 3: //Delete PTR
						break;
				}
			}
		}

		static (IEnumerable<DomainNode> Domains, IEnumerable<IpAddressNode> IpAddresses, IEnumerable<DnsPtrRelationship> DnsPtrs, IEnumerable<DnsChildRelationship> DnsChild) Generate(int count)
		{
			var domains = new HashSet<string>();
			var ips = new HashSet<string>();
			var dnsptrs = new List<DnsPtrRelationship>();
			var dnschilds = new List<DnsChildRelationship>();

			foreach (var tld in SampleData.TopLevelDomains)
			{
				domains.Add(tld);
			}

			while (domains.Count < count)
			{
				// generate random domain
				var index = Random.Next(domains.Count);
				var parent = domains.ElementAt(index);
				var domain = GenerateRandomDomain((v) => domains.Contains(v), parent);

				dnschilds.Add(new DnsChildRelationship() {ParentDomain = parent, ChildDomain = domain});
				domains.Add(domain);


				string ipAddress;
				if (ips.Count == 0 || Random.Next(2) > 0) // two-thirds chance
				{
					// generate random ip address
					ipAddress = GenerateRandomIpAddress((v) => ips.Contains(v));
				}
				else
				{
					// use existing ip address
					index = Random.Next(ips.Count);
					ipAddress = ips.ElementAt(index);
				}

				ips.Add(ipAddress);
				dnsptrs.Add(new DnsPtrRelationship() {Domain = domain, IpAddress = ipAddress});
			}

			return (domains.Select(dn => new DomainNode() {Name = dn}), ips.Select(ip => new IpAddressNode() {IpAddress = ip}), dnsptrs, dnschilds);
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
					switch (Random.Next(2))
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

		private static Regex _regexUnsafeDomainChars = new Regex("[^a-z0-9]", RegexOptions.Compiled | RegexOptions.IgnoreCase);

		static string DomainSafe(string value)
		{
			return _regexUnsafeDomainChars.Replace(value, "");

		}

	}
}