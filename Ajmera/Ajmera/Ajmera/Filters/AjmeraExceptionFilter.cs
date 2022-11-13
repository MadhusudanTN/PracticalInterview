using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using Serilog.Exceptions;

namespace Ajmera.Filters
{
    public class AjmeraExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Serilog.ILogger logger = new LoggerConfiguration()
                               .Enrich.FromLogContext()
                               .Enrich.WithExceptionDetails()
                               .WriteTo.Console()
                            .CreateLogger();

            logger.Error(context.Exception.ToString());
        }
    }
}