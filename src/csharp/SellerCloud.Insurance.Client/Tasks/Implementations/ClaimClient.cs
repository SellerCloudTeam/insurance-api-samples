using SellerCloud.Insurance.Client.Models;
using SellerCloud.Insurance.Client.Models.Requests;
using SellerCloud.Insurance.Client.Tasks.Contracts;
using SellerCloud.Net.Http.Api;
using SellerCloud.Net.Http.Models;
using SellerCloud.Results;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SellerCloud.Insurance.Client.Tasks.Implementations
{
    public class ClaimClient : HttpApiClient, IClaimClient
    {
        public ClaimClient(HttpClient client)
            : base(client, ApiEndpoints.Production)
        {
        }

        public ClaimClient(HttpClient client, string baseUri)
            : base(client, baseUri)
        {
        }

        public Task<Result<IEnumerable<Claim>>> ListClaims(AuthToken token, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.HttpGet($"/api/claims")
                       .AuthorizeWith(token)
                       .CancelWith(cancellationToken)
                       .Result<IEnumerable<Claim>>();
        }

        public Task<Result<Claim>> FileClaimLoss(AuthToken token, string packageId, FileClaimLossRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.HttpPost($"/api/claim/{packageId}/loss", request)
                       .AuthorizeWith(token)
                       .CancelWith(cancellationToken)
                       .Result<Claim>();
        }

        public Task<Result<Claim>> FileClaimDamage(AuthToken token, string packageId, FileClaimDamageRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.HttpPost($"/api/claim/{packageId}/damage", request)
                       .AuthorizeWith(token)
                       .CancelWith(cancellationToken)
                       .Result<Claim>();
        }

        public Task<Result<Claim>> GetClaim(AuthToken token, string claimId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.HttpGet($"/api/claim/{claimId}")
                       .AuthorizeWith(token)
                       .CancelWith(cancellationToken)
                       .Result<Claim>();
        }

        public Task<Result<IEnumerable<ClaimStatusNote>>> GetClaimStatus(AuthToken token, string claimId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.HttpGet($"/api/claim/{claimId}/status")
                       .AuthorizeWith(token)
                       .CancelWith(cancellationToken)
                       .Result<IEnumerable<ClaimStatusNote>>();
        }
    }
}
