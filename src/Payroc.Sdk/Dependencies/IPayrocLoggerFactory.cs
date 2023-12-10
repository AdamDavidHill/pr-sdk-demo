using Microsoft.Extensions.Logging;

namespace Payroc.Sdk.Dependencies;

internal interface IPayrocLoggerFactory
{
    ILogger CreateLogger(string name);
}
