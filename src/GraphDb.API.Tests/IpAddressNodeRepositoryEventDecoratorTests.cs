using GraphDb.API.Decorators;
using GraphDb.API.Domain;
using GraphDb.API.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GraphDb.API.Tests
{
	[TestClass]
	public class IpAddressNodeRepositoryEventDecoratorTests
	{
		[TestMethod]
		public void RemoveByName_IpAddress_RepositoryRemoveByNameAndPublishEvent()
		{
			var mockIpAddressNodeRepository = new Mock<IIpAddressNodeRepository>();
			mockIpAddressNodeRepository.Setup(m => m.RemoveByIpAddress(It.IsAny<string>()));

			var mockHub = new Mock<IEventHub>();

			var decorator = new IpAddressNodeRepositoryEventDecorator(mockIpAddressNodeRepository.Object, mockHub.Object);

			decorator.RemoveByIpAddress("127.0.0.1");


			mockIpAddressNodeRepository.Verify(m => m.RemoveByIpAddress("127.0.0.1"), Times.Once);
			mockHub.Verify(m => m.PublishEvent("Removed", It.IsAny<IpAddressNode>()));
		}

		[TestMethod]
		public void GetByName_IpAddress_RepositoryGetByName()
		{
			var mockDomainNodeRepository = new Mock<IIpAddressNodeRepository>();
			mockDomainNodeRepository.Setup(m => m.GetByIpAddress(It.IsAny<string>())).Returns((string ip) => new IpAddressNode() { IpAddress = ip });

			var mockHub = new Mock<IEventHub>();

			var decorator = new IpAddressNodeRepositoryEventDecorator(mockDomainNodeRepository.Object, mockHub.Object);

			var node = decorator.GetByIpAddress("127.0.0.1");


			mockDomainNodeRepository.Verify(m => m.GetByIpAddress("127.0.0.1"), Times.Once);
			Assert.AreEqual("127.0.0.1", node.IpAddress);
		}
	}
}