using MediatR;
using System.Diagnostics;

namespace CarRentalApp.Logic.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;
        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            //pre-logic
            var requestName = request.GetType();

            _logger.LogInformation("CarRentalApp: logger: {Request} is starting.", requestName);

            var timer = Stopwatch.StartNew();

            //execute pre-logic, once completed -> use next() method to pass the request to the next handler/layer
            var response = await next();


            //post logic 
            timer.Stop();
            _logger.LogInformation("CarRentalApp: logger: {Request} has finished in {Time}ms.", requestName, timer.ElapsedMilliseconds);

            //return response to the next handler/layer
            return response;
        }
    }
}
