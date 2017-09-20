using GraphDb.API.Decorators;
using GraphDb.API.Domain;
using GraphDb.API.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GraphDb.API.Tests
{
	[TestClass]
	public class NodeRepositoryEventDecoratorTests
	{
		[TestMethod]
		public void Remove_Node_RepositoryRemoveAndPublishEvent()
		{
			var mockNodeRepository = new Mock<INodeRepository<INode>>();
			mockNodeRepository.Setup(m => m.Remove(It.IsAny<INode>()));

			var mockHub = new Mock<IEventHub>();

			var decorator = new NodeRepositoryEventDecorator<INode>(mockNodeRepository.Object, mockHub.Object);

			decorator.Remove(new DomainNode() {Name = "example.com"});

			mockNodeRepository.Verify(m => m.Remove(It.IsAny<DomainNode>()), Times.Once);
			mockHub.Verify(m => m.PublishEvent("Removed", It.IsAny<DomainNode>()));
		}

		[TestMethod]
		public void Save_Node_RepositoryRemoveAndPublishEvent()
		{
			var mockNodeRepository = new Mock<INodeRepository<INode>>();
			mockNodeRepository.Setup(m => m.Save(It.IsAny<INode[]>()));

			var mockHub = new Mock<IEventHub>();

			var decorator = new NodeRepositoryEventDecorator<INode>(mockNodeRepository.Object, mockHub.Object);

			decorator.Save(new []{ new DomainNode() { Name = "example.com" } });

			mockNodeRepository.Verify(m => m.Save(It.IsAny<DomainNode[]>()), Times.Once);
			mockHub.Verify(m => m.PublishEvent("Added", It.IsAny<DomainNode[]>()));
		}
	}
}