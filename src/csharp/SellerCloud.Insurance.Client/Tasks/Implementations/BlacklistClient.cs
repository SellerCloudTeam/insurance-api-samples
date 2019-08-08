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
    public class BlacklistClient : HttpApiClient, IBlacklistClient
    {
        public BlacklistClient(HttpClient client)
            : base(client, ApiEndpoints.Production)
        {
        }

        public BlacklistClient(HttpClient client, string baseUri)
            : base(client, baseUri)
        {
        }

        public Task<Result<IEnumerable<BlacklistedCustomer>>> GetBlacklistedCustomers(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.HttpGet($"/api/blacklist/customers")
                       .CancelWith(cancellationToken)
                       .Result<IEnumerable<BlacklistedCustomer>>();
        }

        public Task<Result<IEnumerable<Country>>> GetBlacklistedCountries(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.HttpGet($"/api/blacklist/countries")
                       .CancelWith(cancellationToken)
                       .Result<IEnumerable<Country>>();
        }

        public Task<Result<IEnumerable<Product>>> GetBlacklistedProducts(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.HttpGet($"/api/blacklist/products")
                       .CancelWith(cancellationToken)
                       .Result<IEnumerable<Product>>();
        }
    }
}