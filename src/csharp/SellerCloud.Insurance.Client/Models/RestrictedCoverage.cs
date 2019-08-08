namespace SellerCloud.Insurance.Client.Models
{
    public class RestrictedCoverage
    {
        public decimal? PerParcelLimitOfLiability { get; set; }

        public decimal? PerConveyanceLimitOfLiability { get; set; }

        public bool? SignatureConfirmationRequired { get; set; }
    }
}
