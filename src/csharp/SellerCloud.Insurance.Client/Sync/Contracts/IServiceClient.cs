using SellerCloud.Insurance.Client.Models;
using SellerCloud.Results;
using System.Collections.Generic;

namespace SellerCloud.Insurance.Client.Sync.Contracts
{
    public interface IServiceClient
    {
        Result CheckStatus();

        Result<IEnumerable<PolicyCoverage>> GetCoverage();

        Result<IEnumerable<ProductRestrictedCoverage>> GetRestrictedCoverage();
    }
}