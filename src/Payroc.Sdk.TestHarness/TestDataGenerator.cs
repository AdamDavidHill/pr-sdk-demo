using Payroc.Sdk.Models.Extensions;
using Payroc.Sdk.Models;

internal class TestDataGenerator
{
    public static Merchant CreateExampleMerchant1()
        => new()
        {
            MerchantPlatformId = Guid.NewGuid().ToString(),
            Business = new()
            {
                Name = "Test",
                TaxId = Guid.NewGuid().ToString(),
                OrganizationType = BusinessOrganizationType.PublicCorporation,
                CountryOfOperation = CountryCodeIso3166.US,
                ContactMethods = new()
                {
                    new (ContactMethodType.Email, "a@b.com"),
                    new (ContactMethodType.Phone, "123 456 7890")
                }
            }
        };

    public static Merchant CreateExampleMerchant2()
        => new Merchant()
            .WithMerchantPlatformId(Guid.NewGuid().ToString())
            .WithBusiness("My business", "xxx-xx-4321", BusinessOrganizationType.PublicCorporation, CountryCodeIso3166.US)
            .WithContactMethod(ContactMethodType.Email, "a@b.com")
            .WithContactMethod(ContactMethodType.Phone, "123 456 7890");
}
