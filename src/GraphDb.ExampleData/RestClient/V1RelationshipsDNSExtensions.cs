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
    /// Extension methods for V1RelationshipsDNS.
    /// </summary>
    public static partial class V1RelationshipsDNSExtensions
    {
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='skip'>
            /// </param>
            /// <param name='limit'>
            /// </param>
            public static IList<DnsChildRelationship> CHILDsGet(this IV1RelationshipsDNS operations, int? skip = default(int?), int? limit = default(int?))
            {
                return operations.CHILDsGetAsync(skip, limit).GetAwaiter().GetResult();
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
            public static async Task<IList<DnsChildRelationship>> CHILDsGetAsync(this IV1RelationshipsDNS operations, int? skip = default(int?), int? limit = default(int?), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.CHILDsGetWithHttpMessagesAsync(skip, limit, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='relationships'>
            /// </param>
            public static void CHILDsPost(this IV1RelationshipsDNS operations, IList<DnsChildRelationship> relationships = default(IList<DnsChildRelationship>))
            {
                operations.CHILDsPostAsync(relationships).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='relationships'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task CHILDsPostAsync(this IV1RelationshipsDNS operations, IList<DnsChildRelationship> relationships = default(IList<DnsChildRelationship>), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.CHILDsPostWithHttpMessagesAsync(relationships, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='relationship'>
            /// </param>
            public static void CHILDsDelete(this IV1RelationshipsDNS operations, DnsChildRelationship relationship = default(DnsChildRelationship))
            {
                operations.CHILDsDeleteAsync(relationship).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='relationship'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task CHILDsDeleteAsync(this IV1RelationshipsDNS operations, DnsChildRelationship relationship = default(DnsChildRelationship), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.CHILDsDeleteWithHttpMessagesAsync(relationship, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='skip'>
            /// </param>
            /// <param name='limit'>
            /// </param>
            public static IList<DnsPtrRelationship> PTRsGet(this IV1RelationshipsDNS operations, int? skip = default(int?), int? limit = default(int?))
            {
                return operations.PTRsGetAsync(skip, limit).GetAwaiter().GetResult();
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
            public static async Task<IList<DnsPtrRelationship>> PTRsGetAsync(this IV1RelationshipsDNS operations, int? skip = default(int?), int? limit = default(int?), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.PTRsGetWithHttpMessagesAsync(skip, limit, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='relationships'>
            /// </param>
            public static void PTRsPost(this IV1RelationshipsDNS operations, IList<DnsPtrRelationship> relationships = default(IList<DnsPtrRelationship>))
            {
                operations.PTRsPostAsync(relationships).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='relationships'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task PTRsPostAsync(this IV1RelationshipsDNS operations, IList<DnsPtrRelationship> relationships = default(IList<DnsPtrRelationship>), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.PTRsPostWithHttpMessagesAsync(relationships, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='relationship'>
            /// </param>
            public static void PTRsDelete(this IV1RelationshipsDNS operations, DnsPtrRelationship relationship = default(DnsPtrRelationship))
            {
                operations.PTRsDeleteAsync(relationship).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='relationship'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task PTRsDeleteAsync(this IV1RelationshipsDNS operations, DnsPtrRelationship relationship = default(DnsPtrRelationship), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.PTRsDeleteWithHttpMessagesAsync(relationship, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

    }
}
