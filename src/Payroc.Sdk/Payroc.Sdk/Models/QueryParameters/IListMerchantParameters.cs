namespace Payroc.Sdk.Models.QueryParameters;

public interface IListMerchantParameters
{
    string? After { get; set; }
    string? Before { get; set; }
    int Limit { get; set; }
}
