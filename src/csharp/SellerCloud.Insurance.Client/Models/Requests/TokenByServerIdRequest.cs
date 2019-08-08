namespace SellerCloud.Insurance.Client.Models.Requests
{
    public class TokenByServerIdRequest
    {
        public string ServerId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
