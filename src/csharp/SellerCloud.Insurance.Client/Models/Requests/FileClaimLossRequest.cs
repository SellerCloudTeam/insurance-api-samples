using System;

namespace SellerCloud.Insurance.Client.Models.Requests
{
    public class FileClaimLossRequest : FileClaimRequestBase
    {
        public DateTime LossDiscoveredDate { get; set; }
    }
}
