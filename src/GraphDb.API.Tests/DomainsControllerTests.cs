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
    public class DomainsControllerTests
	{
		private IEnumerable<DomainNode> GenerateSampleNodes(int startIndex=0, int count=100)
		{
			var nodes = new DomainNode[10];
			for (int i = startIndex; i < (startIndex+count); i++)
			{
				yield return new DomainNode() { Name = $"example-{i}.com" };
			}
		}

			
        [TestMethod]
        public void Get_Default_RepositoryAll()
        {
			var mockDomainRepo = new Mock<IDomainNodeRepository>();
	        mockDomainRepo.Setup(m => m.All(It.IsAny<int>(), It.IsAny<int>())).Returns((int skip, int limit) => GenerateSampleNodes().Skip(skip).Take(limit));

			var controller = new DomainsController(mockDomainRepo.Object);
	        var nodes = controller.Get();

	        mockDomainRepo.Verify(e => e.All(0, Int32.MaxValue), Times.Once);
	        Assert.IsTrue(nodes.Count() == 100, "nodes.Count() does not equal 100");
	        Assert.IsTrue(nodes.First().Name == "example-0.com", "first node is not 'example-0.com'");
	        Assert.IsTrue(nodes.Last().Name == "example-99.com", "first node is not 'example-99.com'");
        }

		[TestMethod]
		public void Get_Skip_RepositoryAllWithSkipOnly()
		{
			var mockDomainRepo = new Mock<IDomainNodeRepository>();
			mockDomainRepo.Setup(m => m.All(It.IsAny<int>(), It.IsAny<int>())).Returns((int skip, int limit) => GenerateSampleNodes().Skip(skip).Take(limit));

			var controller = new DomainsController(mockDomainRepo.Object);
			var nodes = controller.Get(skip: 4);

			mockDomainRepo.Verify(e => e.All(4, Int32.MaxValue), Times.Once);
			Assert.IsTrue(nodes.Count() == 96, "nodes.Count() does not equal 96");
			Assert.IsTrue(nodes.First().Name == "example-4.com", "first node is not 'example-4.com'");
			Assert.IsTrue(nodes.Last().Name == "example-99.com", "first node is not 'example-99.com'");
		}

		[TestMethod]
		public void Get_Limit_RepositoryAllWithLimitOnly()
		{
			var mockDomainRepo = new Mock<IDomainNodeRepository>();
			mockDomainRepo.Setup(m => m.All(It.IsAny<int>(), It.IsAny<int>())).Returns((int skip, int limit) => GenerateSampleNodes().Skip(skip).Take(limit));

			var controller = new DomainsController(mockDomainRepo.Object);
			var nodes = controller.Get(limit: 25);

			mockDomainRepo.Verify(e => e.All(0, 25), Times.Once);
			Assert.IsTrue(nodes.Count() == 25, "nodes.Count() does not equal 25");
			Assert.IsTrue(nodes.First().Name == "example-0.com", "first node is not 'example-0.com'");
			Assert.IsTrue(nodes.Last().Name == "example-24.com", "first node is not 'example-24.com'");
		}

		[TestMethod]
		public void Get_SkipAndLimit_RepositoryAllWithSkipAndLimit()
		{
			var mockDomainRepo = new Mock<IDomainNodeRepository>();
			mockDomainRepo.Setup(m => m.All(It.IsAny<int>(), It.IsAny<int>())).Returns((int skip, int limit) => GenerateSampleNodes().Skip(skip).Take(limit));

			var controller = new DomainsController(mockDomainRepo.Object);
			var nodes = controller.Get(skip: 4, limit: 25);

			mockDomainRepo.Verify(e => e.All(4, 25), Times.Once);
			Assert.IsTrue(nodes.Count() == 25, "nodes.Count() does not equal 25");
			Assert.IsTrue(nodes.First().Name == "example-4.com", "first node is not 'example-4.com'");
			Assert.IsTrue(nodes.Last().Name == "example-28.com", "first node is not 'example-28.com'");
		}

		[TestMethod]
		public void Get_Name_RepositoryGetByName()
		{
			var mockDomainRepo = new Mock<IDomainNodeRepository>();
			mockDomainRepo.Setup(m => m.GetByName(It.IsAny<string>())).Returns((string name) => GenerateSampleNodes().FirstOrDefault(n => n.Name == name) );

			var controller = new DomainsController(mockDomainRepo.Object);
			var node = controller.Get("example-4.com");

			mockDomainRepo.Verify(e => e.GetByName("example-4.com"), Times.Once);
			Assert.IsNotNull(node, "node is null");
			Assert.IsTrue(node.Name == "example-4.com", "node is not 'example-4.com'");
		}


		// TODO: Refactor Response handling, to test for 404
		[Ignore]
		[TestMethod]
		public void Get_Name_RepositoryGetByName_Null()
		{
			var mockDomainRepo = new Mock<IDomainNodeRepository>();
			mockDomainRepo.Setup(m => m.GetByName(It.IsAny<string>())).Returns((string name) => GenerateSampleNodes().FirstOrDefault(n => n.Name == name));

			var controller = new DomainsController(mockDomainRepo.Object);
			var node = controller.Get("notfound.com");

			mockDomainRepo.Verify(e => e.GetByName("notfound.com"), Times.Once);
			Assert.IsNull(node, "node is not null");
		}

		[TestMethod]
		public void Post_Nodes_RepositorySave()
		{
			DomainNode[] capture = {};

			var mockDomainRepo = new Mock<IDomainNodeRepository>();
			mockDomainRepo.Setup(m => m.Save(It.IsAny<DomainNode[]>())).Callback((IEnumerable<DomainNode> nodes) => capture = nodes.ToArray());

			var controller = new DomainsController(mockDomainRepo.Object);
			controller.Post(GenerateSampleNodes(count: 5).ToArray());

			mockDomainRepo.Verify(e => e.Save(It.IsAny<DomainNode[]>()), Times.Once);
			Assert.IsTrue(capture.Length == 5, "nodes saved is not 5");
		}

		[TestMethod]
		public void Delete_Name_RepositoryRemove()
		{
			var mockDomainRepo = new Mock<IDomainNodeRepository>();
			mockDomainRepo.Setup(m => m.Remove(It.IsAny<DomainNode>())).Callback((DomainNode node)=> mockDomainRepo.Object.RemoveByName(node.Name));

			var controller = new DomainsController(mockDomainRepo.Object);
			controller.Delete("example-4.com");

			mockDomainRepo.Verify(e => e.RemoveByName("example-4.com"), Times.Once);
		}
	}
}
