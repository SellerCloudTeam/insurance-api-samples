namespace SellerCloud.Insurance.Client.Models
{
    public class ClaimStatusNote
    {
        public string Kind { get; set; }

        public string Note { get; set; }

        public string LastUpdated { get; set; } // Todo: Must change to DateTime after testing Claim Status API
    }
}
