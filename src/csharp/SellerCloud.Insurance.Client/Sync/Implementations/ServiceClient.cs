using SellerCloud.Insurance.Client.Models;
using SellerCloud.Insurance.Client.Sync.Contracts;
using SellerCloud.Net.Http.Api;
using SellerCloud.Results;
using System;
using System.Collections.Generic;
using System.Net;

namespace SellerCloud.Insurance.Client.Sync.Implementations
{
    public class ServiceClient : LegacyApiClient, IServiceClient
    {
        public ServiceClient()
            : base(ApiEndpoints.Production)
        {
        }

        public ServiceClient(string baseUri)
            : base(baseUri)
        {
        }

        public Result CheckStatus()
        {
            return this.HttpGet($"/api/status")
                       .Result();
        }

        public Result<IEnumerable<PolicyCoverage>> GetCoverage()
        {
            return this.HttpGet($"/api/coverage")
                       .Result<IEnumerable<PolicyCoverage>>();
        }

        public Result<IEnumerable<ProductRestrictedCoverage>> GetRestrictedCoverage()
        {
            return this.HttpGet($"/api/coverage/restricted")
                       .Result<IEnumerable<ProductRestrictedCoverage>>();
        }
    }
}