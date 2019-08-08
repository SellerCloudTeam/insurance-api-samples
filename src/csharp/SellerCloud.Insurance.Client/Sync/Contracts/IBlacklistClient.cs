using SellerCloud.Insurance.Client.Models;
using SellerCloud.Results;
using System.Collections.Generic;

namespace SellerCloud.Insurance.Client.Sync.Contracts
{
    public interface IBlacklistClient
    {
        Result<IEnumerable<BlacklistedCustomer>> GetBlacklistedCustomers();

        Result<IEnumerable<Country>> GetBlacklistedCountries();

        Result<IEnumerable<Product>> GetBlacklistedProducts();
    }
}