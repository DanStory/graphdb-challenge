// Code generated by Microsoft (R) AutoRest Code Generator 1.2.2.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Swagger
{
    using Models;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for GraphDbAPI.
    /// </summary>
    public static partial class GraphDbAPIExtensions
    {
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='skip'>
            /// </param>
            /// <param name='limit'>
            /// </param>
            public static IList<DomainNode> V1NodesDomainsGet(this IGraphDbAPI operations, int? skip = default(int?), int? limit = default(int?))
            {
                return operations.V1NodesDomainsGetAsync(skip, limit).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='skip'>
            /// </param>
            /// <param name='limit'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<DomainNode>> V1NodesDomainsGetAsync(this IGraphDbAPI operations, int? skip = default(int?), int? limit = default(int?), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.V1NodesDomainsGetWithHttpMessagesAsync(skip, limit, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='nodes'>
            /// </param>
            public static void V1NodesDomainsPost(this IGraphDbAPI operations, IList<DomainNode> nodes = default(IList<DomainNode>))
            {
                operations.V1NodesDomainsPostAsync(nodes).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='nodes'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task V1NodesDomainsPostAsync(this IGraphDbAPI operations, IList<DomainNode> nodes = default(IList<DomainNode>), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.V1NodesDomainsPostWithHttpMessagesAsync(nodes, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='name'>
            /// </param>
            public static DomainNode V1NodesDomainsByNameGet(this IGraphDbAPI operations, string name)
            {
                return operations.V1NodesDomainsByNameGetAsync(name).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='name'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<DomainNode> V1NodesDomainsByNameGetAsync(this IGraphDbAPI operations, string name, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.V1NodesDomainsByNameGetWithHttpMessagesAsync(name, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='name'>
            /// </param>
            public static void V1NodesDomainsByNameDelete(this IGraphDbAPI operations, string name)
            {
                operations.V1NodesDomainsByNameDeleteAsync(name).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='name'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task V1NodesDomainsByNameDeleteAsync(this IGraphDbAPI operations, string name, CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.V1NodesDomainsByNameDeleteWithHttpMessagesAsync(name, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='skip'>
            /// </param>
            /// <param name='limit'>
            /// </param>
            public static IList<IpAddressNode> V1NodesIpAddressesGet(this IGraphDbAPI operations, int? skip = default(int?), int? limit = default(int?))
            {
                return operations.V1NodesIpAddressesGetAsync(skip, limit).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='skip'>
            /// </param>
            /// <param name='limit'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<IpAddressNode>> V1NodesIpAddressesGetAsync(this IGraphDbAPI operations, int? skip = default(int?), int? limit = default(int?), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.V1NodesIpAddressesGetWithHttpMessagesAsync(skip, limit, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='nodes'>
            /// </param>
            public static void V1NodesIpAddressesPost(this IGraphDbAPI operations, IList<IpAddressNode> nodes = default(IList<IpAddressNode>))
            {
                operations.V1NodesIpAddressesPostAsync(nodes).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='nodes'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task V1NodesIpAddressesPostAsync(this IGraphDbAPI operations, IList<IpAddressNode> nodes = default(IList<IpAddressNode>), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.V1NodesIpAddressesPostWithHttpMessagesAsync(nodes, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='ipAddress'>
            /// </param>
            public static IpAddressNode V1NodesIpAddressesByIpAddressGet(this IGraphDbAPI operations, string ipAddress)
            {
                return operations.V1NodesIpAddressesByIpAddressGetAsync(ipAddress).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='ipAddress'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IpAddressNode> V1NodesIpAddressesByIpAddressGetAsync(this IGraphDbAPI operations, string ipAddress, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.V1NodesIpAddressesByIpAddressGetWithHttpMessagesAsync(ipAddress, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='ipAddress'>
            /// </param>
            public static void V1NodesIpAddressesByIpAddressDelete(this IGraphDbAPI operations, string ipAddress)
            {
                operations.V1NodesIpAddressesByIpAddressDeleteAsync(ipAddress).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='ipAddress'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task V1NodesIpAddressesByIpAddressDeleteAsync(this IGraphDbAPI operations, string ipAddress, CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.V1NodesIpAddressesByIpAddressDeleteWithHttpMessagesAsync(ipAddress, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='skip'>
            /// </param>
            /// <param name='limit'>
            /// </param>
            public static IList<INode> V1NodesGet(this IGraphDbAPI operations, int? skip = default(int?), int? limit = default(int?))
            {
                return operations.V1NodesGetAsync(skip, limit).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='skip'>
            /// </param>
            /// <param name='limit'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<INode>> V1NodesGetAsync(this IGraphDbAPI operations, int? skip = default(int?), int? limit = default(int?), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.V1NodesGetWithHttpMessagesAsync(skip, limit, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='skip'>
            /// </param>
            /// <param name='limit'>
            /// </param>
            public static IList<INodeRelationship> V1RelationshipsGet(this IGraphDbAPI operations, int? skip = default(int?), int? limit = default(int?))
            {
                return operations.V1RelationshipsGetAsync(skip, limit).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='skip'>
            /// </param>
            /// <param name='limit'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<INodeRelationship>> V1RelationshipsGetAsync(this IGraphDbAPI operations, int? skip = default(int?), int? limit = default(int?), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.V1RelationshipsGetWithHttpMessagesAsync(skip, limit, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static void V1StatusGet(this IGraphDbAPI operations)
            {
                operations.V1StatusGetAsync().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task V1StatusGetAsync(this IGraphDbAPI operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.V1StatusGetWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static void V1StatusHead(this IGraphDbAPI operations)
            {
                operations.V1StatusHeadAsync().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task V1StatusHeadAsync(this IGraphDbAPI operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.V1StatusHeadWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

    }
}
