#region Usings
using SellerCloud.Insurance.Client.Models;
using SellerCloud.Insurance.Client.Models.Requests;
using SellerCloud.Insurance.Client.Models.Responses;
using SellerCloud.Insurance.Client.Tasks.Contracts;
using SellerCloud.Insurance.Client.Tasks.Implementations;
using SellerCloud.Net.Http.Models;
using SellerCloud.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static Sample.DotNetCore.Program;
using static Sample.DotNetCore.Sample;
#endregion

namespace Sample.DotNetCore.Tests
{
    static class AsyncClientTest
    {
        public static async Task Run(string baseUri)
        {
            var client = new HttpClient();

            var serviceClient = new ServiceClient(client, baseUri);
            var blacklistClient = new BlacklistClient(client, baseUri);
            var insuredPackageClient = new InsuredPackageClient(client, baseUri);
            var claimClient = new ClaimClient(client, baseUri);
            var authenticationClient = new AuthenticationClient(client, baseUri);

            await RunServiceTests(serviceClient);

            await RunBlacklistTests(blacklistClient);

            var token = await RunTokenTests(authenticationClient);

            var insuredPackages = await RunInsuredPackageTests(insuredPackageClient, token);

            await RunClaimTests(claimClient, token, insuredPackages);

            await RunVoidTests(insuredPackageClient, token, insuredPackages);
        }

        #region Test Pipeline
        static async Task RunServiceTests(ServiceClient serviceClient)
        {
            // Status
            await CheckStatus(serviceClient);

            Log.Line("Insurance Service status OK");

            // Coverage
            var coverage = await GetCoverage(serviceClient);

            Log.Dump("Coverage", coverage);

            // Restricted Coverage
            var restrictedCoverage = await GetRestrictedCoverage(serviceClient);

            Log.Dump("Restricted coverage", restrictedCoverage);
        }

        static async Task RunBlacklistTests(BlacklistClient blacklistClient)
        {
            // Blacklisted countries
            var blacklistedCountries = await GetBlacklistedCountries(blacklistClient);

            Log.Dump("Blacklisted countries", blacklistedCountries);

            // Blacklisted products
            var blacklistedProducts = await GetBlacklistedProducts(blacklistClient);

            Log.Dump("Blacklisted products", blacklistedProducts);
        }

        static async Task<AuthToken> RunTokenTests(AuthenticationClient authenticationClient)
        {
            // Tokens
            var tokenByTeam = await TokenByTeam(authenticationClient, TEAM, USERNAME, PASSWORD);
            var tokenByServerId = await TokenByServerId(authenticationClient, SERVER_ID, USERNAME, PASSWORD);
            var tokenByServerUrl = await TokenByServerUrl(authenticationClient, SERVER_URL, USERNAME, PASSWORD);

            Log.Dump("Token", tokenByTeam);

            var validatedToken = await ValidateToken(authenticationClient, tokenByTeam.access_token);

            Log.Dump("Validated token", validatedToken);

            // Prepare the token for API use
            var token = new AuthToken(tokenByTeam.token_type, tokenByTeam.access_token);

            return token;
        }

        static async Task<IEnumerable<InsuredPackage>> RunInsuredPackageTests(InsuredPackageClient insuredPackageClient, AuthToken token)
        {
            // Insured package
            var insurePackageRequest1 = ConstructSampleInsurePackageRequest("UPS Ground", insuredAmount: 15);
            var insurePackageRequest2 = ConstructSampleInsurePackageRequest("FedEx 2nd Day", insuredAmount: 25);

            // Insure
            var insuredPackage1 = await InsurePackage(insuredPackageClient, token, insurePackageRequest1);
            var insuredPackage2 = await InsurePackage(insuredPackageClient, token, insurePackageRequest2);

            Log.Dump("Insured package 1", insuredPackage1);
            Log.Dump("Insured package 2", insuredPackage2);

            // Negative scenario for insured pacakges
            var insurePackageRequest3 = ConstructSampleInsurePackageRequest("FedEx Ground", insuredAmount: 25000);
            var insurePackageRequest4 = ConstructSampleInsurePackageRequest("FedEx Ground", insuredAmount: 25, description: "Fresh eggs");
            var insurePackageRequest5 = ConstructSampleInsurePackageRequest("Invalid Service", insuredAmount: 25);
            var insurePackageRequest6 = ConstructSampleInsurePackageRequest("FedEx Express Worldwide", insuredAmount: 25, blacklistedCountry: true);
            var insurePackageRequest7 = ConstructSampleInsurePackageRequest("FedEx Ground", insuredAmount: 25, description: "New iPad 3G");
            var insurePackageRequest8 = ConstructSampleInsurePackageRequest("FedEx Ground", insuredAmount: 1500, description: "New iPad 3G");
            var insurePackageRequest9 = ConstructSampleInsurePackageRequest("FedEx Freight", insuredAmount: 250);

            var packageException3 = await MustThrowAsync(() => InsurePackage(insuredPackageClient, token, insurePackageRequest3));
            var packageException4 = await MustThrowAsync(() => InsurePackage(insuredPackageClient, token, insurePackageRequest4));
            var packageException5 = await MustThrowAsync(() => InsurePackage(insuredPackageClient, token, insurePackageRequest5));
            var packageException6 = await MustThrowAsync(() => InsurePackage(insuredPackageClient, token, insurePackageRequest6));
            var packageException7 = await MustThrowAsync(() => InsurePackage(insuredPackageClient, token, insurePackageRequest7));
            var packageException8 = await MustThrowAsync(() => InsurePackage(insuredPackageClient, token, insurePackageRequest8));
            var packageException9 = await MustThrowAsync(() => InsurePackage(insuredPackageClient, token, insurePackageRequest9));

            Log.Line(packageException3.Message);
            Log.Line(packageException4.Message);
            Log.Line(packageException5.Message);
            Log.Line(packageException6.Message);
            Log.Line(packageException7.Message);
            Log.Line(packageException8.Message);
            Log.Line(packageException9.Message);

            // Can insure package?
            var canInsurePackage1 = await CanInsurePackage(insuredPackageClient, token, insurePackageRequest1);
            var canInsurePackage2 = await CanInsurePackage(insuredPackageClient, token, insurePackageRequest2);
            var canInsurePackage3 = await CanInsurePackage(insuredPackageClient, token, insurePackageRequest3);
            var canInsurePackage4 = await CanInsurePackage(insuredPackageClient, token, insurePackageRequest4);
            var canInsurePackage5 = await CanInsurePackage(insuredPackageClient, token, insurePackageRequest5);
            var canInsurePackage6 = await CanInsurePackage(insuredPackageClient, token, insurePackageRequest6);
            var canInsurePackage7 = await CanInsurePackage(insuredPackageClient, token, insurePackageRequest7);
            var canInsurePackage8 = await CanInsurePackage(insuredPackageClient, token, insurePackageRequest8);
            var canInsurePackage9 = await CanInsurePackage(insuredPackageClient, token, insurePackageRequest9);

            Log.Line(canInsurePackage1.IsSuccessful ? "Can insure package 1" : canInsurePackage1.Message);
            Log.Line(canInsurePackage2.IsSuccessful ? "Can insure package 2" : canInsurePackage2.Message);
            Log.Line(canInsurePackage3.IsSuccessful ? "Can insure package 3" : canInsurePackage3.Message);
            Log.Line(canInsurePackage4.IsSuccessful ? "Can insure package 4" : canInsurePackage4.Message);
            Log.Line(canInsurePackage5.IsSuccessful ? "Can insure package 5" : canInsurePackage5.Message);
            Log.Line(canInsurePackage6.IsSuccessful ? "Can insure package 6" : canInsurePackage6.Message);
            Log.Line(canInsurePackage7.IsSuccessful ? "Can insure package 7" : canInsurePackage7.Message);
            Log.Line(canInsurePackage8.IsSuccessful ? "Can insure package 8" : canInsurePackage8.Message);
            Log.Line(canInsurePackage9.IsSuccessful ? "Can insure package 9" : canInsurePackage9.Message);

            // Get
            var freshInsuredPackage1 = await GetInsuredPackage(insuredPackageClient, token, insuredPackage1.PackageId);
            var freshInsuredPackage2 = await GetInsuredPackage(insuredPackageClient, token, insuredPackage2.PackageId);

            // List
            var activeInsuredPackages = await ListActiveInsuredPackages(insuredPackageClient, token);
            var voidedInsuredPackages = await ListVoidedInsuredPackages(insuredPackageClient, token);
            var allInsuredPackages = await ListInsuredPackages(insuredPackageClient, token);

            return new[]
            {
                insuredPackage1,
                insuredPackage2
            };
        }

        static async Task RunClaimTests(ClaimClient claimClient, AuthToken token, IEnumerable<InsuredPackage> insuredPackages)
        {
            var insuredPackage1 = insuredPackages.ElementAt(0);
            var insuredPackage2 = insuredPackages.ElementAt(1);

            // Claim (loss, damage)
            var lossClaim = await FileClaimLoss(claimClient, token, insuredPackage1.PackageId, "Test contents", "Description of discovered loss", lossDiscoveredDate: DateTime.Now);
            var damageClaim = await FileClaimDamage(claimClient, token, insuredPackage2.PackageId, "Test contents", "Description of damage");

            Log.Dump("Claim (loss)", lossClaim);
            Log.Dump("Claim (damage)", damageClaim);

            // Get
            var freshLossClaim = await GetClaim(claimClient, token, lossClaim.ClaimId);
            var freshClaimDamage = await GetClaim(claimClient, token, damageClaim.ClaimId);

            // List all claims
            var claims = await ListClaims(claimClient, token);
        }

        static async Task RunVoidTests(InsuredPackageClient insuredPackageClient, AuthToken token, IEnumerable<InsuredPackage> insuredPackages)
        {
            foreach (var insuredPackage in insuredPackages)
            {
                // Void insured package
                await VoidInsuredPackage(insuredPackageClient, token, insuredPackage.PackageId);

                Log.Line($"Voided package {insuredPackage.PackageId}");
            }
        }

        static async Task<Exception> MustThrowAsync(Func<Task> task)
        {
            Exception exception = null;

            try
            {
                await task();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            if (exception == null)
            {
                throw new InvalidOperationException("The API call should have thrown an error!");
            }

            return exception;
        }
        #endregion

        #region Service
        static async Task CheckStatus(IServiceClient serviceClient)
        {
            var result = await serviceClient.CheckStatus();

            result.ThrowIfUnsuccessful();
        }

        static async Task<IEnumerable<PolicyCoverage>> GetCoverage(IServiceClient serviceClient)
        {
            var result = await serviceClient.GetCoverage();

            return result.ResolveOrThrow();
        }

        static async Task<IEnumerable<ProductRestrictedCoverage>> GetRestrictedCoverage(IServiceClient serviceClient)
        {
            var result = await serviceClient.GetRestrictedCoverage();

            return result.ResolveOrThrow();
        }
        #endregion

        #region Blacklist
        static async Task<IEnumerable<Country>> GetBlacklistedCountries(IBlacklistClient blacklistClient)
        {
            var result = await blacklistClient.GetBlacklistedCountries();

            return result.ResolveOrThrow();
        }

        static async Task<IEnumerable<Product>> GetBlacklistedProducts(IBlacklistClient blacklistClient)
        {
            var result = await blacklistClient.GetBlacklistedProducts();

            return result.ResolveOrThrow();
        }
        #endregion

        #region Authentication
        static async Task<TokenResponse> TokenByTeam(IAuthenticationClient authenticationClient, string team, string username, string password)
        {
            var request = new TokenByTeamRequest
            {
                Team = team,
                Username = username,
                Password = password
            };

            var result = await authenticationClient.TokenByTeam(request);

            return result.ResolveOrThrow();
        }

        static async Task<TokenResponse> TokenByServerId(IAuthenticationClient authenticationClient, string serverId, string username, string password)
        {
            var request = new TokenByServerIdRequest
            {
                ServerId = serverId,
                Username = username,
                Password = password
            };

            var result = await authenticationClient.TokenByServerId(request);

            return result.ResolveOrThrow();
        }

        static async Task<TokenResponse> TokenByServerUrl(IAuthenticationClient authenticationClient, string serverUrl, string username, string password)
        {
            var request = new TokenByServerUrlRequest
            {
                ServerUrl = serverUrl,
                Username = username,
                Password = password
            };

            var result = await authenticationClient.TokenByServerUrl(request);

            return result.ResolveOrThrow();
        }

        static async Task<ValidateTokenResponse> ValidateToken(IAuthenticationClient authenticationClient, string token)
        {
            var request = new ValidateTokenRequest { Token = token };
            var result = await authenticationClient.ValidateToken(request);

            return result.ResolveOrThrow();
        }
        #endregion

        #region Insured Package
        static async Task<Result> CanInsurePackage(IInsuredPackageClient insuredPackageClient, AuthToken token, InsurePackageRequest request)
        {
            var result = await insuredPackageClient.CanInsurePackage(token, request);

            return result;
        }

        static async Task<InsuredPackage> InsurePackage(IInsuredPackageClient insuredPackageClient, AuthToken token, InsurePackageRequest request)
        {
            var result = await insuredPackageClient.InsurePackage(token, request);

            return result.ResolveOrThrow();
        }

        static async Task<InsuredPackage> GetInsuredPackage(IInsuredPackageClient insuredPackageClient, AuthToken token, string packageId)
        {
            var result = await insuredPackageClient.GetInsuredPackage(token, packageId);

            return result.ResolveOrThrow();
        }

        static async Task<IEnumerable<InsuredPackage>> ListActiveInsuredPackages(IInsuredPackageClient insuredPackageClient, AuthToken token)
        {
            var result = await insuredPackageClient.ListActiveInsuredPackages(token);

            return result.ResolveOrThrow();
        }

        static async Task<IEnumerable<InsuredPackage>> ListVoidedInsuredPackages(IInsuredPackageClient insuredPackageClient, AuthToken token)
        {
            var result = await insuredPackageClient.ListVoidedInsuredPackages(token);

            return result.ResolveOrThrow();
        }

        static async Task<IEnumerable<InsuredPackage>> ListInsuredPackages(IInsuredPackageClient insuredPackageClient, AuthToken token)
        {
            var result = await insuredPackageClient.ListInsuredPackages(token);

            return result.ResolveOrThrow();
        }

        static async Task VoidInsuredPackage(IInsuredPackageClient insuredPackageClient, AuthToken token, string packageId)
        {
            var result = await insuredPackageClient.VoidInsuredPackage(token, packageId);

            result.ThrowIfUnsuccessful();
        }
        #endregion

        #region Claim
        static async Task<Claim> FileClaimLoss(IClaimClient claimClient, AuthToken token, string packageId, string contents, string description, DateTime lossDiscoveredDate)
        {
            var request = new FileClaimLossRequest
            {
                Contents = contents,
                Description = description,
                LossDiscoveredDate = lossDiscoveredDate
            };

            var result = await claimClient.FileClaimLoss(token, packageId, request);

            return result.ResolveOrThrow();
        }

        static async Task<Claim> FileClaimDamage(IClaimClient claimClient, AuthToken token, string packageId, string contents, string description)
        {
            var request = new FileClaimDamageRequest
            {
                Contents = contents,
                Description = description
            };

            var result = await claimClient.FileClaimDamage(token, packageId, request);

            return result.ResolveOrThrow();
        }

        static async Task<Claim> GetClaim(IClaimClient claimClient, AuthToken token, string claimId)
        {
            var result = await claimClient.GetClaim(token, claimId);

            return result.ResolveOrThrow();
        }

        static async Task<IEnumerable<Claim>> ListClaims(IClaimClient claimClient, AuthToken token)
        {
            var result = await claimClient.ListClaims(token);

            return result.ResolveOrThrow();
        }
        #endregion
    }
}