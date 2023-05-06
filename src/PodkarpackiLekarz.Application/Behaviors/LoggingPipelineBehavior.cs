using MediatR;
using Microsoft.Extensions.Logging;

namespace PodkarpackiLekarz.Application.Behaviours;
public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started handling: {command}", typeof(TRequest).Name);

        var response = await next();

        _logger.LogInformation("Finished handling: {command}", typeof(TRequest).Name);

        return response;
    }
}
