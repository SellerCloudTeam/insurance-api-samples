namespace SellerCloud.Insurance.Client.Models.Requests
{
    public class InsurePackageRequest
    {
        public Shipper Shipper { get; set; }

        public Customer Customer { get; set; }

        public PackageDetails Details { get; set; }

        public PhysicalAddress Origin { get; set; }

        public PhysicalAddress Destination { get; set; }
    }
}
