namespace Payroc.Sdk;

public class IdempotencyKey
{
    public IdempotencyKey() => Value = Guid.NewGuid().ToString();

    public static IdempotencyKey New() => new();

    internal string Value { get; }
}
