using Payroc.Sdk.Web;

namespace Payroc.Sdk;

// We ensure our main entry point has an interface so:
// - It's easier for other people to mock
// - It's a nice clean summary/expression of what the implementation is all about
public interface IPayrocService
{
    IPayrocSession CreateSession();
    string CreateIdempotencyToken() => Guid.NewGuid().ToString().ToUpper();
}
