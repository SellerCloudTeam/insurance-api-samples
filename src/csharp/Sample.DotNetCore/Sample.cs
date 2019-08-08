using SellerCloud.Insurance.Client;
using SellerCloud.Insurance.Client.Models;
using SellerCloud.Insurance.Client.Models.Requests;
using System;

namespace Sample.DotNetCore
{
    static class Sample
    {
        public static InsurePackageRequest ConstructSampleInsurePackageRequest(string shippingMethod, decimal insuredAmount, string description = null, bool blacklistedCountry = false)
        {
            return new InsurePackageRequest
            {
                Shipper = ConstructSampleShipper(),
                Customer = ConstructSampleCustomer(),
                Details = ConstructSamplePackageDetails(shippingMethod, insuredAmount, description),
                Origin = ConstructSampleUsAddress(),
                Destination = blacklistedCountry
                    ? ConstructSampleRussianAddress()
                    : ConstructSampleUsAddress()
            };
        }

        static Shipper ConstructSampleShipper()
        {
            return new Shipper
            {
                CompanyName = "Sample Company",
                FirstName = "John",
                LastName = "Smith",
                Phone = "+359 123 456 789",
                EmailAddress = "shipper@sample-company.com"
            };
        }

        static Customer ConstructSampleCustomer()
        {
            return new Customer
            {
                ReferenceId = "marina_p",
                Email = "customer@sample-company.com",
                FirstName = "Marina",
                LastName = "Peterson"
            };
        }

        static PackageDetails ConstructSamplePackageDetails(string shippingMethod, decimal insuredAmount, string description = null)
        {
            return new PackageDetails
            {
                ShipmentId = "test_shipment",
                PackageId = $"{DateTime.Now.Ticks}",
                DescriptionOfContents = description ?? "Not real package",
                TrackingNumber = "NOT_REAL_PACKAGE",
                ShippingMethod = shippingMethod,
                InsuredAmount = insuredAmount
            };
        }

        static PhysicalAddress ConstructSampleUsAddress()
        {
            return new PhysicalAddress
            {
                StreetLine1 = "1600 Amphitheatre Parkway",
                StreetLine2 = "Ste 123",
                City = "Mountain View",
                State = "CA",
                PostalCode = "94043",
                Country = "US"
            };
        }

        static PhysicalAddress ConstructSampleRussianAddress()
        {
            return new PhysicalAddress
            {
                StreetLine1 = "Marksa K. Ul., bld. 137, appt. 30",
                City = "Krasnoyarsk",
                State = "Krasnoyarskiy kray",
                PostalCode = "9999",
                Country = "Russia"
            };
        }
    }
}
