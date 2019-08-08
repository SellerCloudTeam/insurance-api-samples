using SellerCloud.Insurance.Client.Models;
using SellerCloud.Insurance.Client.Models.Requests;
using SellerCloud.Net.Http.Models;
using SellerCloud.Results;
using System.Collections.Generic;

namespace SellerCloud.Insurance.Client.Sync.Contracts
{
    public interface IClaimClient
    {
        Result<IEnumerable<Claim>> ListClaims(AuthToken token);

        Result<Claim> FileClaimLoss(AuthToken token, string packageId, FileClaimLossRequest request);

        Result<Claim> FileClaimDamage(AuthToken token, string packageId, FileClaimDamageRequest request);

        Result<Claim> GetClaim(AuthToken token, string claimId);

        Result<IEnumerable<ClaimStatusNote>> GetClaimStatus(AuthToken token, string claimId);
    }
}