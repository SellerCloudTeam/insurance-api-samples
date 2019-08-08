using Sample.DotNetCore.Tests;
using SellerCloud.Insurance.Client;
using System;
using System.Threading.Tasks;

namespace Sample.DotNetCore
{
    static class Program
    {
        // The Insurance API sandbox tests acquire tokens
        // by team name, server ID, and server URL
        //
        // The default is to use a token by team name but
        // you can change this to any of the two other options

        // TODO: Enter your SellerCloud server ID and Web Services URL here
        //       e.g. https://<server-id>.ws.sellercloud.com
        public const string SERVER_ID = your_server_id;
        public const string SERVER_URL = your_server_url;

        // TODO: Enter your SellerCloud team name and credentials here
        public const string TEAM = your_team_name;
        public const string USERNAME = your_email_address;
        public const string PASSWORD = your_password;

        // Asynchronous (Task) test
        static async Task Main()
        {
            await AsyncClientTest.Run(ApiEndpoints.Sandbox); // This uses the *sandbox* endpoint

            WaitForAnyKey();
        }

        // Legacy (synchronous) test
        // static void Main()
        // {
        //     SyncClientTest.Run(ApiEndpoints.Sandbox); // This uses the *sandbox* endpoint
        // 
        //     WaitForAnyKey();
        // }

        static void WaitForAnyKey()
        {
            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }
    }
}
