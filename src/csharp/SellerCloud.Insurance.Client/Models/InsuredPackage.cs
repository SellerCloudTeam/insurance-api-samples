using System;

namespace SellerCloud.Insurance.Client.Models
{
    public class InsuredPackage
    {
        // public InsurerKind Insurer { get; set; }

        public string PackageId { get; set; }

        public Shipper Shipper { get; set; }

        public Customer Customer { get; set; }

        public InsuredPackageDetails Details { get; set; }

        public PhysicalAddress Origin { get; set; }

        public PhysicalAddress Destination { get; set; }

        public InsuredPackageCoverage Coverage { get; set; }

        public string ShipmentPackaging { get; set; }

        public DateTime ShipmentDate { get; set; }

        public InsuredPackageReferences References { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? VoidedAt { get; set; }

        public string VoidedBy { get; set; }

        public DateTime? ExportedAt { get; set; }

        public DateTime? BilledAt { get; set; }
    }
}
