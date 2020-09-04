using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Serilog;

namespace Competitor.Middleware
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            try
            {
                return _next(httpContext);
            }
            catch (Exception e)
            {
                //Log.Logger.Error(e, "unhandled exception has occurred!");
                throw;
            }
            
        }
    }

    public static class LogMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogMiddleware>();
        }
    }
}
