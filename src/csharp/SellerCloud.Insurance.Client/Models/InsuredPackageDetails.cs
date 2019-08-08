namespace SellerCloud.Insurance.Client
{
    public class InsuredPackageDetails
    {
        public string ShipmentId { get; set; }

        public string PackageId { get; set; }

        public string DescriptionOfContents { get; set; }

        public string TrackingNumber { get; set; }

        public string ShippingMethod { get; set; }

        public decimal InsuredAmount { get; set; }
    }
}