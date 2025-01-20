using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ClassLib.Behaviors;
public class LoggingBehavior<TRequest, TResponse>
    (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation($"[START] LogHandle Request={typeof(TRequest).Name}, Response={typeof(TRequest).Name}, RequestData={request}");

        var timer = new Stopwatch();
        timer.Start();

        var response = await next();

        timer.Stop();

        var timeTaken = timer.Elapsed;

        logger.LogInformation($"[INFO] Time Taken for the Request = {timeTaken}");

        logger.LogInformation($"[END] LogHandle Request={typeof(TRequest).Name}, Response={typeof(TRequest).Name}, ResponseData={response}");

        return response;
    }
}

