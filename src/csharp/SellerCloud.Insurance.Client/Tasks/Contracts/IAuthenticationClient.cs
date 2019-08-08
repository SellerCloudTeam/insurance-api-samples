using SellerCloud.Insurance.Client.Models.Requests;
using SellerCloud.Insurance.Client.Models.Responses;
using SellerCloud.Results;
using System.Threading;
using System.Threading.Tasks;

namespace SellerCloud.Insurance.Client.Tasks.Contracts
{
    public interface IAuthenticationClient
    {
        Task<Result<TokenResponse>> TokenByTeam(TokenByTeamRequest request, CancellationToken cancellationToken = default(CancellationToken));

        Task<Result<TokenResponse>> TokenByServerId(TokenByServerIdRequest request, CancellationToken cancellationToken = default(CancellationToken));

        Task<Result<TokenResponse>> TokenByServerUrl(TokenByServerUrlRequest request, CancellationToken cancellationToken = default(CancellationToken));

        Task<Result<ValidateTokenResponse>> ValidateToken(ValidateTokenRequest request, CancellationToken cancellationToken = default(CancellationToken));
    }
}