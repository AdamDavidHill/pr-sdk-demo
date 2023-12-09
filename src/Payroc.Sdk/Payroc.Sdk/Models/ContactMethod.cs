namespace Payroc.Sdk.Models;

public class ContactMethod
{
    public ContactMethod() { }

    public ContactMethod(ContactMethodType type, string value)
        => (Type, Value)
        = (type, value);

    public ContactMethodType Type { get; set; }

    public string Value { get; set; } = string.Empty;
}
