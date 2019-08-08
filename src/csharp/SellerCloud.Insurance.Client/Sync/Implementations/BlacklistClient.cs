using SellerCloud.Insurance.Client.Models;
using SellerCloud.Insurance.Client.Sync.Contracts;
using SellerCloud.Net.Http.Api;
using SellerCloud.Results;
using System.Collections.Generic;

namespace SellerCloud.Insurance.Client.Sync.Implementations
{
    public class BlacklistClient : LegacyApiClient, IBlacklistClient
    {
        public BlacklistClient()
            : base(ApiEndpoints.Production)
        {
        }

        public BlacklistClient(string baseUri)
            : base(baseUri)
        {
        }

        public Result<IEnumerable<Country>> GetBlacklistedCountries()
        {
            return this.HttpGet($"/api/blacklist/countries")
                       .Result<IEnumerable<Country>>();
        }

        public Result<IEnumerable<Product>> GetBlacklistedProducts()
        {
            return this.HttpGet($"/api/blacklist/products")
                       .Result<IEnumerable<Product>>();
        }
    }
}