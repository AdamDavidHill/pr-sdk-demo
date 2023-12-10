using Microsoft.Extensions.Logging;

namespace Payroc.Sdk.Web;

internal interface IPayrocLoggerFactory
{
    ILogger CreateLogger(string name);
}
