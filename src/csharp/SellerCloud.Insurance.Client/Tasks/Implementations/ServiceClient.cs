using SellerCloud.Insurance.Client.Models;
using SellerCloud.Insurance.Client.Tasks.Contracts;
using SellerCloud.Net.Http.Api;
using SellerCloud.Results;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SellerCloud.Insurance.Client.Tasks.Implementations
{
    public class ServiceClient : HttpApiClient, IServiceClient
    {
        public ServiceClient(HttpClient client)
            : base(client, ApiEndpoints.Production)
        {
        }

        public ServiceClient(HttpClient client, string baseUri)
            : base(client, baseUri)
        {
        }

        public Task<Result> CheckStatus(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.HttpGet($"/api/status")
                       .CancelWith(cancellationToken)
                       .Result();
        }

        public Task<Result<IEnumerable<PolicyCoverage>>> GetCoverage(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.HttpGet($"/api/coverage")
                       .CancelWith(cancellationToken)
                       .Result<IEnumerable<PolicyCoverage>>();
        }

        public Task<Result<IEnumerable<ProductRestrictedCoverage>>> GetRestrictedCoverage(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.HttpGet($"/api/coverage/restricted")
                       .CancelWith(cancellationToken)
                       .Result<IEnumerable<ProductRestrictedCoverage>>();
        }
    }
}