using System.Collections.Generic;
using GraphDb.API.Domain;

namespace GraphDb.API.Repositories
{
	public interface INodeReadOnlyRepository<out TNode> where TNode : INode
	{
		IEnumerable<TNode> All(int skip, int limit);
	}

	public interface INodeRepository<TNode> : INodeReadOnlyRepository<TNode> where TNode:INode
	{
		void Save(IEnumerable<TNode> nodes);
		void Remove(TNode node);
	}

	public interface INodeRelationshipReadOnlyRepository<out TNodeRelationship> where TNodeRelationship : INodeRelationship
	{
		IEnumerable<TNodeRelationship> All(int skip, int limit);
	}

	public interface INodeRelationshipRepository<TNodeRelationship> : INodeRelationshipReadOnlyRepository<TNodeRelationship> where TNodeRelationship : INodeRelationship
	{
		void Save(IEnumerable<TNodeRelationship> relationship);
		void Remove(TNodeRelationship relationship);

	}
}