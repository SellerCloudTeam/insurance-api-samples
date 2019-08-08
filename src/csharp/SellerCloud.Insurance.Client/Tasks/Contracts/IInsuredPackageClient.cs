using SellerCloud.Insurance.Client.Models;
using SellerCloud.Insurance.Client.Models.Requests;
using SellerCloud.Net.Http.Models;
using SellerCloud.Results;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SellerCloud.Insurance.Client.Tasks.Contracts
{
    public interface IInsuredPackageClient
    {
        Task<Result<IEnumerable<InsuredPackage>>> ListInsuredPackages(AuthToken token, CancellationToken cancellationToken = default(CancellationToken));

        Task<Result<IEnumerable<InsuredPackage>>> ListActiveInsuredPackages(AuthToken token, CancellationToken cancellationToken = default(CancellationToken));

        Task<Result<IEnumerable<InsuredPackage>>> ListVoidedInsuredPackages(AuthToken token, CancellationToken cancellationToken = default(CancellationToken));

        Task<Result> CanInsurePackage(AuthToken token, InsurePackageRequest request, CancellationToken cancellationToken = default(CancellationToken));

        Task<Result<InsuredPackage>> InsurePackage(AuthToken token, InsurePackageRequest request, CancellationToken cancellationToken = default(CancellationToken));

        Task<Result<InsuredPackage>> GetInsuredPackage(AuthToken token, string packageId, CancellationToken cancellationToken = default(CancellationToken));

        Task<Result> VoidInsuredPackage(AuthToken token, string packageId, CancellationToken cancellationToken = default(CancellationToken));
    }
}