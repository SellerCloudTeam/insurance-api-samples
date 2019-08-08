using System;

namespace SellerCloud.Insurance.Client.Models
{
    public class Claim
    {
        // public InsurerKind Insurer { get; set; }

        public string ClaimId { get; set; }

        public string AdditionalId { get; set; }

        public string Contents { get; set; }

        public string Description { get; set; }

        public DateTime? LossDiscoveredDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public InsuredPackage InsuredPackage { get; set; }
    }
}
