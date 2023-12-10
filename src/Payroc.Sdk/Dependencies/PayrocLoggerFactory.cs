using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Payroc.Sdk.Dependencies;

// Wraps the handling of ILogger so we can make DI and manual construction solutions interchangeable
// We trade off some of the typed richness of not using ILogger<T>, but compensate a little using named loggers when made via DI
internal partial class PayrocLoggerFactory : IPayrocLoggerFactory
{
    private readonly ILoggerFactory? _loggerFactory;
    private readonly ILogger? _logger;

    public PayrocLoggerFactory(ILoggerFactory loggerFactory) => _loggerFactory = loggerFactory;

    protected PayrocLoggerFactory(ILogger logger) => _logger = logger;

    public ILogger CreateLogger(string name) => _logger ?? _loggerFactory?.CreateLogger(name) ?? NullLogger.Instance;

    public static PayrocLoggerFactory Create(ILogger? logger) => new PayrocLoggerFactory(logger ?? NullLogger.Instance);
}
