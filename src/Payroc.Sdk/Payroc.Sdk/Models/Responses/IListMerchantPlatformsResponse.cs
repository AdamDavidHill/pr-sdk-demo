namespace Payroc.Sdk.Models.Responses;

public interface IListMerchantPlatformsResponse
{
    int Count { get; }
    Merchant[] Data { get; }
    int HasMore { get; }
    int Limit { get; }
}
