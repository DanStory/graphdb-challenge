using GraphDb.API.Decorators;
using GraphDb.API.Domain;
using GraphDb.API.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GraphDb.API.Tests
{
	[TestClass]
	public class NodeRelationshipRepositoryEventDecoratorTests
	{
		[TestMethod]
		public void Remove_Relationship_RepositoryRemoveAndPublishEvent()
		{
			var mockNodeRepository = new Mock<INodeRelationshipRepository<INodeRelationship>>();
			mockNodeRepository.Setup(m => m.Remove(It.IsAny<INodeRelationship>()));

			var mockHub = new Mock<IEventHub>();

			var decorator = new NodeRelationshipRepositoryEventDecorator<INodeRelationship>(mockNodeRepository.Object, mockHub.Object);

			decorator.Remove(new DnsPtrRelationship() {Domain = "example.com", IpAddress = "127.0.0.1"});

			mockNodeRepository.Verify(m => m.Remove(It.IsAny<DnsPtrRelationship>()), Times.Once);
			mockHub.Verify(m => m.PublishEvent("Removed", It.IsAny<DnsPtrRelationship>()));
		}

		[TestMethod]
		public void Save_Relationship_RepositoryRemoveAndPublishEvent()
		{
			var mockNodeRepository = new Mock<INodeRelationshipRepository<INodeRelationship>>();
			mockNodeRepository.Setup(m => m.Save(It.IsAny<INodeRelationship[]>()));

			var mockHub = new Mock<IEventHub>();

			var decorator = new NodeRelationshipRepositoryEventDecorator<INodeRelationship>(mockNodeRepository.Object, mockHub.Object);

			decorator.Save(new[] { new DnsPtrRelationship() { Domain = "example.com", IpAddress = "127.0.0.1" } });

			mockNodeRepository.Verify(m => m.Save(It.IsAny<DnsPtrRelationship[]>()), Times.Once);
			mockHub.Verify(m => m.PublishEvent("Added", It.IsAny<DnsPtrRelationship[]>()));
		}
	}
}