namespace SellerCloud.Insurance.Client.Models
{
    public class PolicyCoverage
    {
        public string Destination { get; set; }

        public string ShippingMethod { get; set; }

        public string Description { get; set; }

        public decimal ShippingCarrierLiabilityDeductible { get; set; }

        public decimal PerParcelLimitOfLiability { get; set; }

        public decimal PerConveyanceLimitOfLiability { get; set; }
    }
}
