using SellerCloud.Insurance.Client.Models.Requests;
using SellerCloud.Insurance.Client.Models.Responses;
using SellerCloud.Insurance.Client.Sync.Contracts;
using SellerCloud.Net.Http.Api;
using SellerCloud.Results;
using System.Net;

namespace SellerCloud.Insurance.Client.Sync.Implementations
{
    public class AuthenticationClient : LegacyApiClient, IAuthenticationClient
    {
        public AuthenticationClient()
            : base(ApiEndpoints.Production)
        {
        }

        public AuthenticationClient(string baseUri)
            : base(baseUri)
        {
        }

        public Result<TokenResponse> TokenByTeam(TokenByTeamRequest request)
        {
            return this.HttpPost($"/api/token", request)
                       .Result<TokenResponse>();
        }

        public Result<TokenResponse> TokenByServerId(TokenByServerIdRequest request)
        {
            return this.HttpPost($"/api/token/by-server-id", request)
                       .Result<TokenResponse>();
        }

        public Result<TokenResponse> TokenByServerUrl(TokenByServerUrlRequest request)
        {
            return this.HttpPost($"/api/token/by-server-url", request)
                       .Result<TokenResponse>();
        }

        public Result<ValidateTokenResponse> ValidateToken(ValidateTokenRequest request)
        {
            return this.HttpPost($"/api/token/validate", request)
                       .Result<ValidateTokenResponse>();
        }
    }
}