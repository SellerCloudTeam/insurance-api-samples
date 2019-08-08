using System;

namespace SellerCloud.Insurance.Client.Models.Responses
{
    public class TokenResponse
    {
        public string token_type { get; set; }

        public string access_token { get; set; }

        public DateTime? validFrom { get; set; }

        public DateTime? validTo { get; set; }
    }
}
