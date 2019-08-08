#region Usings
using SellerCloud.Insurance.Client.Models;
using SellerCloud.Insurance.Client.Models.Requests;
using SellerCloud.Insurance.Client.Models.Responses;
using SellerCloud.Insurance.Client.Sync.Contracts;
using SellerCloud.Insurance.Client.Sync.Implementations;
using SellerCloud.Net.Http.Models;
using SellerCloud.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using static Sample.DotNetCore.Program;
using static Sample.DotNetCore.Sample;
#endregion

namespace Sample.DotNetCore.Tests
{
    static class SyncClientTest
    {
        public static void Run(string baseUri)
        {
            var serviceClient = new ServiceClient(baseUri);
            var blacklistClient = new BlacklistClient(baseUri);
            var insuredPackageClient = new InsuredPackageClient(baseUri);
            var claimClient = new ClaimClient(baseUri);
            var authenticationClient = new AuthenticationClient(baseUri);

            RunServiceTests(serviceClient);

            RunBlacklistTests(blacklistClient);

            var token = RunTokenTests(authenticationClient);

            var insuredPackages = RunInsuredPackageTests(insuredPackageClient, token);

            RunClaimTests(claimClient, token, insuredPackages);

            RunVoidTests(insuredPackageClient, token, insuredPackages);
        }

        #region Test Pipeline
        static void RunServiceTests(ServiceClient serviceClient)
        {
            // Status
            CheckStatus(serviceClient);

            Log.Line("Insurance Service status OK");

            // Coverage
            var coverage = GetCoverage(serviceClient);

            Log.Dump("Coverage", coverage);

            // Restricted Coverage
            var restrictedCoverage = GetRestrictedCoverage(serviceClient);

            Log.Dump("Restricted coverage", restrictedCoverage);
        }

        static void RunBlacklistTests(BlacklistClient blacklistClient)
        {
            // Blacklisted countries
            var blacklistedCountries = GetBlacklistedCountries(blacklistClient);

            Log.Dump("Blacklisted countries", blacklistedCountries);

            // Blacklisted products
            var blacklistedProducts = GetBlacklistedProducts(blacklistClient);

            Log.Dump("Blacklisted products", blacklistedProducts);
        }

        static AuthToken RunTokenTests(AuthenticationClient authenticationClient)
        {
            // Tokens
            var tokenByTeam = TokenByTeam(authenticationClient, TEAM, USERNAME, PASSWORD);
            var tokenByServerId = TokenByServerId(authenticationClient, SERVER_ID, USERNAME, PASSWORD);
            var tokenByServerUrl = TokenByServerUrl(authenticationClient, SERVER_URL, USERNAME, PASSWORD);

            Log.Dump("Token", tokenByTeam);

            var validatedToken = ValidateToken(authenticationClient, tokenByTeam.access_token);

            Log.Dump("Validated token", validatedToken);

            // Prepare the token for API use
            var token = new AuthToken(tokenByTeam.token_type, tokenByTeam.access_token);

            return token;
        }

        static IEnumerable<InsuredPackage> RunInsuredPackageTests(InsuredPackageClient insuredPackageClient, AuthToken token)
        {
            // Insured package
            var insurePackageRequest1 = ConstructSampleInsurePackageRequest("UPS Ground", insuredAmount: 15);
            var insurePackageRequest2 = ConstructSampleInsurePackageRequest("FedEx 2nd Day", insuredAmount: 25);

            // Insure
            var insuredPackage1 = InsurePackage(insuredPackageClient, token, insurePackageRequest1);
            var insuredPackage2 = InsurePackage(insuredPackageClient, token, insurePackageRequest2);

            Log.Dump("Insured package 1", insuredPackage1);
            Log.Dump("Insured package 2", insuredPackage2);

            // Negative scenario for insured pacakges
            var insurePackageRequest3 = ConstructSampleInsurePackageRequest("FedEx Ground", insuredAmount: 25000);
            var insurePackageRequest4 = ConstructSampleInsurePackageRequest("FedEx Ground", insuredAmount: 25, description: "Fresh eggs");
            var insurePackageRequest5 = ConstructSampleInsurePackageRequest("Invalid Service", insuredAmount: 25);
            var insurePackageRequest6 = ConstructSampleInsurePackageRequest("FedEx Express Worldwide", insuredAmount: 25, blacklistedCountry: true);
            var insurePackageRequest7 = ConstructSampleInsurePackageRequest("FedEx Ground", insuredAmount: 25, description: "New iPad 3G");
            var insurePackageRequest8 = ConstructSampleInsurePackageRequest("FedEx Ground", insuredAmount: 1500, description: "New iPad 3G");

            var packageException3 = MustThrow(() => InsurePackage(insuredPackageClient, token, insurePackageRequest3));
            var packageException4 = MustThrow(() => InsurePackage(insuredPackageClient, token, insurePackageRequest4));
            var packageException5 = MustThrow(() => InsurePackage(insuredPackageClient, token, insurePackageRequest5));
            var packageException6 = MustThrow(() => InsurePackage(insuredPackageClient, token, insurePackageRequest6));
            var packageException7 = MustThrow(() => InsurePackage(insuredPackageClient, token, insurePackageRequest7));
            var packageException8 = MustThrow(() => InsurePackage(insuredPackageClient, token, insurePackageRequest8));

            Log.Line(packageException3.Message);
            Log.Line(packageException4.Message);
            Log.Line(packageException5.Message);
            Log.Line(packageException6.Message);
            Log.Line(packageException7.Message);
            Log.Line(packageException8.Message);

            // Can insure package?
            var canInsurePackage1 = CanInsurePackage(insuredPackageClient, token, insurePackageRequest1);
            var canInsurePackage2 = CanInsurePackage(insuredPackageClient, token, insurePackageRequest2);
            var canInsurePackage3 = CanInsurePackage(insuredPackageClient, token, insurePackageRequest3);
            var canInsurePackage4 = CanInsurePackage(insuredPackageClient, token, insurePackageRequest4);
            var canInsurePackage5 = CanInsurePackage(insuredPackageClient, token, insurePackageRequest5);
            var canInsurePackage6 = CanInsurePackage(insuredPackageClient, token, insurePackageRequest6);
            var canInsurePackage7 = CanInsurePackage(insuredPackageClient, token, insurePackageRequest7);
            var canInsurePackage8 = CanInsurePackage(insuredPackageClient, token, insurePackageRequest8);

            Log.Line(canInsurePackage1.IsSuccessful ? "Can insure package 1" : canInsurePackage1.Message);
            Log.Line(canInsurePackage2.IsSuccessful ? "Can insure package 2" : canInsurePackage2.Message);
            Log.Line(canInsurePackage3.IsSuccessful ? "Can insure package 3" : canInsurePackage3.Message);
            Log.Line(canInsurePackage4.IsSuccessful ? "Can insure package 4" : canInsurePackage4.Message);
            Log.Line(canInsurePackage5.IsSuccessful ? "Can insure package 5" : canInsurePackage5.Message);
            Log.Line(canInsurePackage6.IsSuccessful ? "Can insure package 6" : canInsurePackage6.Message);
            Log.Line(canInsurePackage7.IsSuccessful ? "Can insure package 7" : canInsurePackage7.Message);
            Log.Line(canInsurePackage8.IsSuccessful ? "Can insure package 8" : canInsurePackage8.Message);

            // Get
            var freshInsuredPackage1 = GetInsuredPackage(insuredPackageClient, token, insuredPackage1.PackageId);
            var freshInsuredPackage2 = GetInsuredPackage(insuredPackageClient, token, insuredPackage2.PackageId);

            // List
            var activeInsuredPackages = ListActiveInsuredPackages(insuredPackageClient, token);
            var voidedInsuredPackages = ListVoidedInsuredPackages(insuredPackageClient, token);
            var allInsuredPackages = ListInsuredPackages(insuredPackageClient, token);

            return new[]
            {
                insuredPackage1,
                insuredPackage2
            };
        }

        static void RunClaimTests(ClaimClient claimClient, AuthToken token, IEnumerable<InsuredPackage> insuredPackages)
        {
            var insuredPackage1 = insuredPackages.ElementAt(0);
            var insuredPackage2 = insuredPackages.ElementAt(1);

            // Claim (loss, damage)
            var lossClaim = FileClaimLoss(claimClient, token, insuredPackage1.PackageId, "Test contents", "Description of discovered loss", lossDiscoveredDate: DateTime.Now);
            var damageClaim = FileClaimDamage(claimClient, token, insuredPackage2.PackageId, "Test contents", "Description of damage");

            Log.Dump("Claim (loss)", lossClaim);
            Log.Dump("Claim (damage)", damageClaim);

            // Get
            var freshLossClaim = GetClaim(claimClient, token, lossClaim.ClaimId);
            var freshClaimDamage = GetClaim(claimClient, token, damageClaim.ClaimId);

            // List all claims
            var claims = ListClaims(claimClient, token);
        }

        static void RunVoidTests(InsuredPackageClient insuredPackageClient, AuthToken token, IEnumerable<InsuredPackage> insuredPackages)
        {
            foreach (var insuredPackage in insuredPackages)
            {
                // Void insured package
                VoidInsuredPackage(insuredPackageClient, token, insuredPackage.PackageId);

                Log.Line($"Voided package {insuredPackage.PackageId}");
            }
        }

        static Exception MustThrow(Action action)
        {
            Exception exception = null;

            try
            {
                action();
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
        static void CheckStatus(IServiceClient serviceClient)
        {
            var result = serviceClient.CheckStatus();

            result.ThrowIfUnsuccessful();
        }

        static IEnumerable<PolicyCoverage> GetCoverage(IServiceClient serviceClient)
        {
            var result = serviceClient.GetCoverage();

            return result.ResolveOrThrow();
        }

        static IEnumerable<ProductRestrictedCoverage> GetRestrictedCoverage(IServiceClient serviceClient)
        {
            var result = serviceClient.GetRestrictedCoverage();

            return result.ResolveOrThrow();
        }
        #endregion

        #region Blacklist
        static IEnumerable<Country> GetBlacklistedCountries(IBlacklistClient blacklistClient)
        {
            var result = blacklistClient.GetBlacklistedCountries();

            return result.ResolveOrThrow();
        }

        static IEnumerable<Product> GetBlacklistedProducts(IBlacklistClient blacklistClient)
        {
            var result = blacklistClient.GetBlacklistedProducts();

            return result.ResolveOrThrow();
        }
        #endregion

        #region Authentication
        static TokenResponse TokenByTeam(IAuthenticationClient authenticationClient, string team, string username, string password)
        {
            var request = new TokenByTeamRequest
            {
                Team = team,
                Username = username,
                Password = password
            };

            var result = authenticationClient.TokenByTeam(request);

            return result.ResolveOrThrow();
        }

        static TokenResponse TokenByServerId(IAuthenticationClient authenticationClient, string serverId, string username, string password)
        {
            var request = new TokenByServerIdRequest
            {
                ServerId = serverId,
                Username = username,
                Password = password
            };

            var result = authenticationClient.TokenByServerId(request);

            return result.ResolveOrThrow();
        }

        static TokenResponse TokenByServerUrl(IAuthenticationClient authenticationClient, string serverUrl, string username, string password)
        {
            var request = new TokenByServerUrlRequest
            {
                ServerUrl = serverUrl,
                Username = username,
                Password = password
            };

            var result = authenticationClient.TokenByServerUrl(request);

            return result.ResolveOrThrow();
        }

        static ValidateTokenResponse ValidateToken(IAuthenticationClient authenticationClient, string token)
        {
            var request = new ValidateTokenRequest { Token = token };
            var result = authenticationClient.ValidateToken(request);

            return result.ResolveOrThrow();
        }
        #endregion

        #region Insured Package
        static Result CanInsurePackage(IInsuredPackageClient insuredPackageClient, AuthToken token, InsurePackageRequest request)
        {
            var result = insuredPackageClient.CanInsurePackage(token, request);

            return result;
        }

        static InsuredPackage InsurePackage(IInsuredPackageClient insuredPackageClient, AuthToken token, InsurePackageRequest request)
        {
            var result = insuredPackageClient.InsurePackage(token, request);

            return result.ResolveOrThrow();
        }

        static InsuredPackage GetInsuredPackage(IInsuredPackageClient insuredPackageClient, AuthToken token, string packageId)
        {
            var result = insuredPackageClient.GetInsuredPackage(token, packageId);

            return result.ResolveOrThrow();
        }

        static IEnumerable<InsuredPackage> ListActiveInsuredPackages(IInsuredPackageClient insuredPackageClient, AuthToken token)
        {
            var result = insuredPackageClient.ListActiveInsuredPackages(token);

            return result.ResolveOrThrow();
        }

        static IEnumerable<InsuredPackage> ListVoidedInsuredPackages(IInsuredPackageClient insuredPackageClient, AuthToken token)
        {
            var result = insuredPackageClient.ListVoidedInsuredPackages(token);

            return result.ResolveOrThrow();
        }

        static IEnumerable<InsuredPackage> ListInsuredPackages(IInsuredPackageClient insuredPackageClient, AuthToken token)
        {
            var result = insuredPackageClient.ListInsuredPackages(token);

            return result.ResolveOrThrow();
        }

        static void VoidInsuredPackage(IInsuredPackageClient insuredPackageClient, AuthToken token, string packageId)
        {
            var result = insuredPackageClient.VoidInsuredPackage(token, packageId);

            result.ThrowIfUnsuccessful();
        }
        #endregion

        #region Claim
        static Claim FileClaimLoss(IClaimClient claimClient, AuthToken token, string packageId, string contents, string description, DateTime lossDiscoveredDate)
        {
            var request = new FileClaimLossRequest
            {
                Contents = contents,
                Description = description,
                LossDiscoveredDate = lossDiscoveredDate
            };

            var result = claimClient.FileClaimLoss(token, packageId, request);

            return result.ResolveOrThrow();
        }

        static Claim FileClaimDamage(IClaimClient claimClient, AuthToken token, string packageId, string contents, string description)
        {
            var request = new FileClaimDamageRequest
            {
                Contents = contents,
                Description = description
            };

            var result = claimClient.FileClaimDamage(token, packageId, request);

            return result.ResolveOrThrow();
        }

        static Claim GetClaim(IClaimClient claimClient, AuthToken token, string claimId)
        {
            var result = claimClient.GetClaim(token, claimId);

            return result.ResolveOrThrow();
        }

        static IEnumerable<Claim> ListClaims(IClaimClient claimClient, AuthToken token)
        {
            var result = claimClient.ListClaims(token);

            return result.ResolveOrThrow();
        }
        #endregion
    }
}