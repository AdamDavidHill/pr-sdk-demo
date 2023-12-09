namespace Payroc.Sdk.Models.QueryParameters;

internal class ListMerchantParameters : IListMerchantParameters
{
    public string? Before { get; set; }

    public string? After { get; set; }

    public int Limit { get; set; } = 10;
}
