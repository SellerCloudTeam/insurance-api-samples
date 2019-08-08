using System.Collections.Generic;

namespace SellerCloud.Insurance.Client.Models
{
    public class BlacklistedCustomer
    {
        public IEnumerable<string> EmailAddresses { get; set; }

        public IEnumerable<string> PhoneNumbers { get; set; }

        public IEnumerable<CustomerId> OtherIds { get; set; }
    }
}
