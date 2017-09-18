using System;
using System.Collections.Generic;
using System.Linq;
using GraphDb.API.Controllers;
using GraphDb.API.Domain;
using GraphDb.API.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GraphDb.API.Tests
{
    [TestClass]
    public class IpAddressesControllerTests
	{
		private IEnumerable<IpAddressNode> GenerateSampleNodes(int startIndex=0, int count=100)
		{
			var nodes = new IpAddressNode[10];
			for (int i = startIndex; i < (startIndex+count); i++)
			{
				yield return new IpAddressNode() { IpAddress = $"127.0.0.{i}" };
			}
		}

			
        [TestMethod]
        public void Get_Default_RepositoryAll()
        {
			var mockRepo = new Mock<IIpAddressNodeRepository>();
	        mockRepo.Setup(m => m.All(It.IsAny<int>(), It.IsAny<int>())).Returns((int skip, int limit) => GenerateSampleNodes().Skip(skip).Take(limit));

			var controller = new IpAddressesController(mockRepo.Object);
	        var nodes = controller.Get();

	        mockRepo.Verify(e => e.All(0, Int32.MaxValue), Times.Once);
	        Assert.IsTrue(nodes.Count() == 100, "nodes.Count() does not equal 100");
	        Assert.IsTrue(nodes.First().IpAddress == "127.0.0.0", "first node is not '127.0.0.0'");
	        Assert.IsTrue(nodes.Last().IpAddress == "127.0.0.99", "first node is not '127.0.0.99'");
        }

		[TestMethod]
		public void Get_Skip_RepositoryAllWithSkipOnly()
		{
			var mockRepo = new Mock<IIpAddressNodeRepository>();
			mockRepo.Setup(m => m.All(It.IsAny<int>(), It.IsAny<int>())).Returns((int skip, int limit) => GenerateSampleNodes().Skip(skip).Take(limit));

			var controller = new IpAddressesController(mockRepo.Object);
			var nodes = controller.Get(skip: 4);

			mockRepo.Verify(e => e.All(4, Int32.MaxValue), Times.Once);
			Assert.IsTrue(nodes.Count() == 96, "nodes.Count() does not equal 96");
			Assert.IsTrue(nodes.First().IpAddress == "127.0.0.4", "first node is not '127.0.0.4'");
			Assert.IsTrue(nodes.Last().IpAddress == "127.0.0.99", "first node is not '127.0.0.99'");
		}

		[TestMethod]
		public void Get_Limit_RepositoryAllWithLimitOnly()
		{
			var mockRepo = new Mock<IIpAddressNodeRepository>();
			mockRepo.Setup(m => m.All(It.IsAny<int>(), It.IsAny<int>())).Returns((int skip, int limit) => GenerateSampleNodes().Skip(skip).Take(limit));

			var controller = new IpAddressesController(mockRepo.Object);
			var nodes = controller.Get(limit: 25);

			mockRepo.Verify(e => e.All(0, 25), Times.Once);
			Assert.IsTrue(nodes.Count() == 25, "nodes.Count() does not equal 25");
			Assert.IsTrue(nodes.First().IpAddress == "127.0.0.0", "first node is not '127.0.0.0'");
			Assert.IsTrue(nodes.Last().IpAddress == "127.0.0.24", "first node is not '127.0.0.24'");
		}

		[TestMethod]
		public void Get_SkipAndLimit_RepositoryAllWithSkipAndLimit()
		{
			var mockRepo = new Mock<IIpAddressNodeRepository>();
			mockRepo.Setup(m => m.All(It.IsAny<int>(), It.IsAny<int>())).Returns((int skip, int limit) => GenerateSampleNodes().Skip(skip).Take(limit));

			var controller = new IpAddressesController(mockRepo.Object);
			var nodes = controller.Get(skip: 4, limit: 25);

			mockRepo.Verify(e => e.All(4, 25), Times.Once);
			Assert.IsTrue(nodes.Count() == 25, "nodes.Count() does not equal 25");
			Assert.IsTrue(nodes.First().IpAddress == "127.0.0.4", "first node is not '127.0.0.4'");
			Assert.IsTrue(nodes.Last().IpAddress == "127.0.0.28", "first node is not '127.0.0.28'");
		}

		[TestMethod]
		public void Get_Name_RepositoryGetByName()
		{
			var mockRepo = new Mock<IIpAddressNodeRepository>();
			mockRepo.Setup(m => m.GetByIpAddress(It.IsAny<string>())).Returns((string ip) => GenerateSampleNodes().FirstOrDefault(n => n.IpAddress == ip) );

			var controller = new IpAddressesController(mockRepo.Object);
			var node = controller.Get("127.0.0.4");

			mockRepo.Verify(e => e.GetByIpAddress("127.0.0.4"), Times.Once);
			Assert.IsNotNull(node, "node is null");
			Assert.IsTrue(node.IpAddress == "127.0.0.4", "node is not '127.0.0.4'");
		}


		// TODO: Refactor Response handling, to test for 404
		[Ignore]
		[TestMethod]
		public void Get_Name_RepositoryGetByName_Null()
		{
			var mockRepo = new Mock<IIpAddressNodeRepository>();
			mockRepo.Setup(m => m.GetByIpAddress(It.IsAny<string>())).Returns((string ip) => GenerateSampleNodes().FirstOrDefault(n => n.IpAddress == ip));

			var controller = new IpAddressesController(mockRepo.Object);
			var node = controller.Get("0.0.0.0");

			mockRepo.Verify(e => e.GetByIpAddress("0.0.0.0"), Times.Once);
			Assert.IsNull(node, "node is not null");
		}

		[TestMethod]
		public void Post_Nodes_RepositorySave()
		{
			IpAddressNode[] capture = {};

			var mockDomainRepo = new Mock<IIpAddressNodeRepository>();
			mockDomainRepo.Setup(m => m.Save(It.IsAny<IpAddressNode[]>())).Callback((IEnumerable<IpAddressNode> nodes) => capture = nodes.ToArray());

			var controller = new IpAddressesController(mockDomainRepo.Object);
			controller.Post(GenerateSampleNodes(count: 5).ToArray());

			mockDomainRepo.Verify(e => e.Save(It.IsAny<IpAddressNode[]>()), Times.Once);
			Assert.IsTrue(capture.Length == 5, "nodes saved is not 5");
		}

		[TestMethod]
		public void Delete_Name_RepositoryRemove()
		{
			var mockDomainRepo = new Mock<IIpAddressNodeRepository>();
			mockDomainRepo.Setup(m => m.Remove(It.IsAny<IpAddressNode>())).Callback((IpAddressNode node)=> mockDomainRepo.Object.RemoveByIpAddress(node.IpAddress));

			var controller = new IpAddressesController(mockDomainRepo.Object);
			controller.Delete("127.0.0.4");

			mockDomainRepo.Verify(e => e.RemoveByIpAddress("127.0.0.4"), Times.Once);
		}
	}
}
