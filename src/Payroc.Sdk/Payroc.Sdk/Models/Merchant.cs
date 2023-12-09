namespace Payroc.Sdk.Models;

public class Merchant
{
    // Would use `get; set;` for public models rather than `get; init;` so as not to frustrate users used to relying on mutation
    public string? MerchantPlatformId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public Business? Business { get; set; }

    // etc.
}
