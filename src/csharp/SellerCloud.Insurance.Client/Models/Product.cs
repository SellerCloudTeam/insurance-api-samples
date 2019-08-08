using System.Collections.Generic;

namespace SellerCloud.Insurance.Client.Models
{
    public class Product
    {
        public string Description { get; set; }

        public IEnumerable<string> Keywords { get; set; }
    }
}
