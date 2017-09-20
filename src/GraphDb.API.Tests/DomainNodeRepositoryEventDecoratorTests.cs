using System;
using System.Collections.Generic;
using System.Text;
using GraphDb.API.Decorators;
using GraphDb.API.Domain;
using GraphDb.API.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GraphDb.API.Tests
{
	[TestClass]
	public class DomainNodeRepositoryEventDecoratorTests
    {
	    [TestMethod]
	    public void RemoveByName_Domain_RepositoryRemoveByNameAndPublishEvent()
	    {
		    var mockDomainNodeRepository = new Mock<IDomainNodeRepository>();
		    mockDomainNodeRepository.Setup(m => m.RemoveByName(It.IsAny<string>()));

			var mockHub = new Mock<IEventHub>();

		    var decorator = new DomainNodeRepositoryEventDecorator(mockDomainNodeRepository.Object, mockHub.Object);

			decorator.RemoveByName("example.com");


			mockDomainNodeRepository.Verify(m => m.RemoveByName("example.com"), Times.Once);
		    mockHub.Verify(m => m.PublishEvent("Removed", It.IsAny<DomainNode>()));
	    }

	    [TestMethod]
	    public void GetByName_Domain_RepositoryGetByName()
	    {
		    var mockDomainNodeRepository = new Mock<IDomainNodeRepository>();
		    mockDomainNodeRepository.Setup(m => m.GetByName(It.IsAny<string>())).Returns((string name) => new DomainNode() { Name = name });

		    var mockHub = new Mock<IEventHub>();

		    var decorator = new DomainNodeRepositoryEventDecorator(mockDomainNodeRepository.Object, mockHub.Object);

		    var node = decorator.GetByName("example.com");


		    mockDomainNodeRepository.Verify(m => m.GetByName("example.com"), Times.Once);
			Assert.AreEqual("example.com", node.Name);
	    }
	}
}
