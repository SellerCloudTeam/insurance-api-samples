using SellerCloud.Insurance.Client.Models;
using SellerCloud.Insurance.Client.Models.Requests;
using SellerCloud.Insurance.Client.Sync.Contracts;
using SellerCloud.Net.Http.Api;
using SellerCloud.Net.Http.Models;
using SellerCloud.Results;
using System.Collections.Generic;
using System.Net;

namespace SellerCloud.Insurance.Client.Sync.Implementations
{
    public class InsuredPackageClient : LegacyApiClient, IInsuredPackageClient
    {
        public InsuredPackageClient()
            : base(ApiEndpoints.Production)
        {
        }

        public InsuredPackageClient(string baseUri)
            : base(baseUri)
        {
        }

        public Result<IEnumerable<InsuredPackage>> ListInsuredPackages(AuthToken token)
        {
            return this.HttpGet($"/api/insured-packages")
                       .AuthorizeWith(token)
                       .Result<IEnumerable<InsuredPackage>>();
        }

        public Result<IEnumerable<InsuredPackage>> ListActiveInsuredPackages(AuthToken token)
        {
            return this.HttpGet($"/api/insured-packages/active")
                       .AuthorizeWith(token)
                       .Result<IEnumerable<InsuredPackage>>();
        }

        public Result<IEnumerable<InsuredPackage>> ListVoidedInsuredPackages(AuthToken token)
        {
            return this.HttpGet($"/api/insured-packages/voided")
                       .AuthorizeWith(token)
                       .Result<IEnumerable<InsuredPackage>>();
        }

        public Result CanInsurePackage(AuthToken token, InsurePackageRequest request)
        {
            return this.HttpPost($"/api/insured-package/test", request)
                       .AuthorizeWith(token)
                       .Result();
        }

        public Result<InsuredPackage> InsurePackage(AuthToken token, InsurePackageRequest request)
        {
            return this.HttpPost($"/api/insured-package", request)
                       .AuthorizeWith(token)
                       .Result<InsuredPackage>();
        }

        public Result<InsuredPackage> GetInsuredPackage(AuthToken token, string packageId)
        {
            return this.HttpGet($"/api/insured-package/{packageId}")
                       .AuthorizeWith(token)
                       .Result<InsuredPackage>();
        }

        public Result VoidInsuredPackage(AuthToken token, string packageId)
        {
            return this.HttpPost($"/api/insured-package/{packageId}/void")
                       .AuthorizeWith(token)
                       .Result();
        }
    }
}
