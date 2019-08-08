namespace SellerCloud.Insurance.Client.Models
{
    public class PackageDetails
    {
        public string ShipmentId { get; set; }

        public string PackageId { get; set; }

        public string DescriptionOfContents { get; set; }

        public string TrackingNumber { get; set; }

        public string ShippingMethod { get; set; }

        public bool SignatureConfirmationRequested { get; set; }

        public decimal InsuredAmount { get; set; }
    }
}
