namespace Payroc.Sdk.Models;

public class Business
{
    public Business() { }

    public Business(string? name, string? taxId = null, BusinessOrganizationType type = BusinessOrganizationType.PrivateCorporation, CountryCodeIso3166? countryOfOperation = null) 
        => (Name, TaxId, OrganizationType, CountryOfOperation)
        = (name, taxId, type, countryOfOperation);

    public string? Name { get; set; }
    
    public string? TaxId { get; set; }

    public BusinessOrganizationType OrganizationType { get; set; }

    public CountryCodeIso3166? CountryOfOperation { get; set; }

    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;

    // etc.

    public List<ContactMethod> ContactMethods { get; set; } = new();
}
