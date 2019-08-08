using System;

namespace SellerCloud.Insurance.Client.Models.Responses
{
    public class ValidateTokenResponse
    {
        public bool IsValid { get; set; }

        public int? UserId { get; set; }

        public int? ClientId { get; set; }

        public string UserName { get; set; }

        public string ServerId { get; set; }

        public string Team { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        public string[] Roles { get; set; }
    }
}
