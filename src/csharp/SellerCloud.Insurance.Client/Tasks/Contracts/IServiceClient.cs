using SellerCloud.Insurance.Client.Models;
using SellerCloud.Results;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SellerCloud.Insurance.Client.Tasks.Contracts
{
    public interface IServiceClient
    {
        Task<Result> CheckStatus(CancellationToken cancellationToken = default(CancellationToken));

        Task<Result<IEnumerable<PolicyCoverage>>> GetCoverage(CancellationToken cancellationToken = default(CancellationToken));

        Task<Result<IEnumerable<ProductRestrictedCoverage>>> GetRestrictedCoverage(CancellationToken cancellationToken = default(CancellationToken));
    }
}