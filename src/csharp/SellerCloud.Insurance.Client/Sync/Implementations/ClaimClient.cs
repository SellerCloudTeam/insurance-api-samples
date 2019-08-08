using SellerCloud.Insurance.Client.Models;
using SellerCloud.Insurance.Client.Models.Requests;
using SellerCloud.Insurance.Client.Sync.Contracts;
using SellerCloud.Net.Http.Api;
using SellerCloud.Net.Http.Models;
using SellerCloud.Results;
using System.Collections.Generic;
using System.Net;

namespace SellerCloud.Insurance.Client.Sync.Implementations
{
    public class ClaimClient : LegacyApiClient, IClaimClient
    {
        public ClaimClient()
            : base(ApiEndpoints.Production)
        {
        }

        public ClaimClient(string baseUri)
            : base(baseUri)
        {
        }

        public Result<IEnumerable<Claim>> ListClaims(AuthToken token)
        {
            return this.HttpGet($"/api/claims")
                       .AuthorizeWith(token)
                       .Result<IEnumerable<Claim>>();
        }

        public Result<Claim> FileClaimLoss(AuthToken token, string packageId, FileClaimLossRequest request)
        {
            return this.HttpPost($"/api/claim/{packageId}/loss", request)
                       .AuthorizeWith(token)
                       .Result<Claim>();
        }

        public Result<Claim> FileClaimDamage(AuthToken token, string packageId, FileClaimDamageRequest request)
        {
            return this.HttpPost($"/api/claim/{packageId}/damage", request)
                       .AuthorizeWith(token)
                       .Result<Claim>();
        }

        public Result<Claim> GetClaim(AuthToken token, string claimId)
        {
            return this.HttpGet($"/api/claim/{claimId}")
                       .AuthorizeWith(token)
                       .Result<Claim>();
        }

        public Result<IEnumerable<ClaimStatusNote>> GetClaimStatus(AuthToken token, string claimId)
        {
            return this.HttpGet($"/api/claim/{claimId}/status")
                       .AuthorizeWith(token)
                       .Result<IEnumerable<ClaimStatusNote>>();
        }
    }
}
