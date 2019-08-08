using SellerCloud.Insurance.Client.Models.Requests;
using SellerCloud.Insurance.Client.Models.Responses;
using SellerCloud.Insurance.Client.Tasks.Contracts;
using SellerCloud.Net.Http.Api;
using SellerCloud.Results;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SellerCloud.Insurance.Client.Tasks.Implementations
{
    public class AuthenticationClient : HttpApiClient, IAuthenticationClient
    {
        public AuthenticationClient(HttpClient client)
            : base(client, ApiEndpoints.Production)
        {
        }

        public AuthenticationClient(HttpClient client, string baseUri)
            : base(client, baseUri)
        {
        }

        public Task<Result<TokenResponse>> TokenByTeam(TokenByTeamRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.HttpPost($"/api/token", request)
                       .CancelWith(cancellationToken)
                       .Result<TokenResponse>();
        }

        public Task<Result<TokenResponse>> TokenByServerId(TokenByServerIdRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.HttpPost($"/api/token/by-server-id", request)
                       .CancelWith(cancellationToken)
                       .Result<TokenResponse>();
        }

        public Task<Result<TokenResponse>> TokenByServerUrl(TokenByServerUrlRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.HttpPost($"/api/token/by-server-url", request)
                       .CancelWith(cancellationToken)
                       .Result<TokenResponse>();
        }

        public Task<Result<ValidateTokenResponse>> ValidateToken(ValidateTokenRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.HttpPost($"/api/token/validate", request)
                       .CancelWith(cancellationToken)
                       .Result<ValidateTokenResponse>();
        }
    }
}