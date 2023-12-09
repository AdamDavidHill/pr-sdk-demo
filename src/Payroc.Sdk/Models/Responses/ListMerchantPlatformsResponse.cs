namespace Payroc.Sdk.Models.Responses;

// Using classes rather than records for response models, even though intended as immutable, as may lead to inadvertant insecure logging via record's default `ToString()` behaviour
// Immutability provded via limited interface definition (no set / init)
public class ListMerchantPlatformsResponse : IListMerchantPlatformsResponse
{
    public int Limit { get; init; }
    public int Count { get; init; }
    public int HasMore { get; init; }

    // etc.

    public Merchant[] Data { get; init; } = Array.Empty<Merchant>();
}
