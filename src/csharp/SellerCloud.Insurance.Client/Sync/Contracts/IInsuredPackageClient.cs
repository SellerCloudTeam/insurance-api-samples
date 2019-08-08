using SellerCloud.Insurance.Client.Models;
using SellerCloud.Insurance.Client.Models.Requests;
using SellerCloud.Net.Http.Models;
using SellerCloud.Results;
using System.Collections.Generic;

namespace SellerCloud.Insurance.Client.Sync.Contracts
{
    public interface IInsuredPackageClient
    {
        Result<IEnumerable<InsuredPackage>> ListInsuredPackages(AuthToken token);

        Result<IEnumerable<InsuredPackage>> ListActiveInsuredPackages(AuthToken token);

        Result<IEnumerable<InsuredPackage>> ListVoidedInsuredPackages(AuthToken token);

        Result CanInsurePackage(AuthToken token, InsurePackageRequest request);

        Result<InsuredPackage> InsurePackage(AuthToken token, InsurePackageRequest request);

        Result<InsuredPackage> GetInsuredPackage(AuthToken token, string packageId);

        Result VoidInsuredPackage(AuthToken token, string packageId);
    }
}