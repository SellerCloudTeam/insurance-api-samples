using SellerCloud.Insurance.Client.Models;
using SellerCloud.Insurance.Client.Models.Requests;
using SellerCloud.Insurance.Client.Tasks.Contracts;
using SellerCloud.Net.Http.Api;
using SellerCloud.Net.Http.Models;
using SellerCloud.Results;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SellerCloud.Insurance.Client.Tasks.Implementations
{
    public class InsuredPackageClient : HttpApiClient, IInsuredPackageClient
    {
        public InsuredPackageClient(HttpClient client)
            : base(client, ApiEndpoints.Production)
        {
        }

        public InsuredPackageClient(HttpClient client, string baseUri)
            : base(client, baseUri)
        {
        }

        public Task<Result<IEnumerable<InsuredPackage>>> ListInsuredPackages(AuthToken token, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.HttpGet($"/api/insured-packages")
                       .AuthorizeWith(token)
                       .CancelWith(cancellationToken)
                       .Result<IEnumerable<InsuredPackage>>();
        }

        public Task<Result<IEnumerable<InsuredPackage>>> ListActiveInsuredPackages(AuthToken token, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.HttpGet($"/api/insured-packages/active")
                       .AuthorizeWith(token)
                       .CancelWith(cancellationToken)
                       .Result<IEnumerable<InsuredPackage>>();
        }

        public Task<Result<IEnumerable<InsuredPackage>>> ListVoidedInsuredPackages(AuthToken token, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.HttpGet($"/api/insured-packages/voided")
                       .AuthorizeWith(token)
                       .CancelWith(cancellationToken)
                       .Result<IEnumerable<InsuredPackage>>();
        }

        public Task<Result> CanInsurePackage(AuthToken token, InsurePackageRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.HttpPost($"/api/insured-package/test", request)
                       .AuthorizeWith(token)
                       .CancelWith(cancellationToken)
                       .Result();
        }

        public Task<Result<InsuredPackage>> InsurePackage(AuthToken token, InsurePackageRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.HttpPost($"/api/insured-package", request)
                       .AuthorizeWith(token)
                       .CancelWith(cancellationToken)
                       .Result<InsuredPackage>();
        }

        public Task<Result<InsuredPackage>> GetInsuredPackage(AuthToken token, string packageId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.HttpGet($"/api/insured-package/{packageId}")
                       .AuthorizeWith(token)
                       .CancelWith(cancellationToken)
                       .Result<InsuredPackage>();
        }

        public Task<Result> VoidInsuredPackage(AuthToken token, string packageId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.HttpPost($"/api/insured-package/{packageId}/void")
                       .AuthorizeWith(token)
                       .CancelWith(cancellationToken)
                       .Result();
        }
    }
}
