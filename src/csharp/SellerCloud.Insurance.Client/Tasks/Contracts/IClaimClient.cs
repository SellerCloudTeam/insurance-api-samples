using SellerCloud.Insurance.Client.Models;
using SellerCloud.Insurance.Client.Models.Requests;
using SellerCloud.Net.Http.Models;
using SellerCloud.Results;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SellerCloud.Insurance.Client.Tasks.Contracts
{
    public interface IClaimClient
    {
        Task<Result<IEnumerable<Claim>>> ListClaims(AuthToken token, CancellationToken cancellationToken = default(CancellationToken));

        Task<Result<Claim>> FileClaimLoss(AuthToken token, string packageId, FileClaimLossRequest request, CancellationToken cancellationToken = default(CancellationToken));

        Task<Result<Claim>> FileClaimDamage(AuthToken token, string packageId, FileClaimDamageRequest request, CancellationToken cancellationToken = default(CancellationToken));

        Task<Result<Claim>> GetClaim(AuthToken token, string claimId, CancellationToken cancellationToken = default(CancellationToken));

        Task<Result<IEnumerable<ClaimStatusNote>>> GetClaimStatus(AuthToken token, string claimId, CancellationToken cancellationToken = default(CancellationToken));
    }
}