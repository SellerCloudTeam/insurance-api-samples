using SellerCloud.Insurance.Client.Models;
using SellerCloud.Results;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SellerCloud.Insurance.Client.Tasks.Contracts
{
    public interface IBlacklistClient
    {
        Task<Result<IEnumerable<Country>>> GetBlacklistedCountries(CancellationToken cancellationToken = default(CancellationToken));

        Task<Result<IEnumerable<Product>>> GetBlacklistedProducts(CancellationToken cancellationToken = default(CancellationToken));
    }
}