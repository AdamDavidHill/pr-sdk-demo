namespace Payroc.Sdk.Models.Extensions;

public static class MerchantExtensions
{
    public static Merchant WithMerchantPlatformId(this Merchant merchant, string merchantPlatformId)
    {
        merchant ??= new();
        merchant.MerchantPlatformId = merchantPlatformId;

        return merchant;
    }

    public static Merchant WithBusiness(this Merchant merchant, string name)
    {
        merchant ??= new();
        merchant.Business ??= new();
        merchant.Business.Name = name;

        return merchant;
    }

    public static Merchant WithBusiness(this Merchant merchant, string name, string taxId, BusinessOrganizationType type, CountryCodeIso3166 countryOfOperation)
    {
        merchant ??= new();
        merchant.Business ??= new();
        merchant.Business.Name = name;
        merchant.Business.TaxId = taxId;
        merchant.Business.OrganizationType = type;
        merchant.Business.CountryOfOperation = countryOfOperation;

        return merchant;
    }

    public static Merchant WithContactMethod(this Merchant merchant, ContactMethodType type, string value)
    {
        merchant ??= new();
        merchant.Business ??= new();
        merchant.Business.ContactMethods ??= new();
        merchant.Business.ContactMethods.Add(new(type, value));

        return merchant;
    }

    // etc.
}
