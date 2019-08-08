using SellerCloud.Insurance.Client.Models.Requests;
using SellerCloud.Insurance.Client.Models.Responses;
using SellerCloud.Results;

namespace SellerCloud.Insurance.Client.Sync.Contracts
{
    public interface IAuthenticationClient
    {
        Result<TokenResponse> TokenByTeam(TokenByTeamRequest request);

        Result<TokenResponse> TokenByServerId(TokenByServerIdRequest request);

        Result<TokenResponse> TokenByServerUrl(TokenByServerUrlRequest request);

        Result<ValidateTokenResponse> ValidateToken(ValidateTokenRequest request);
    }
}