using Communicate.ErrorHandling.Rest.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace GraphQL.Api.Middleware
{
    /// <summary>
    /// Used to pass exceptions from data layer to Api
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            if (ex is NotFoundException)
                code = HttpStatusCode.NotFound;
            else if (ex is UnauthorizedException)
                code = HttpStatusCode.Unauthorized;
            else if (ex is BadRequestException)
                code = HttpStatusCode.BadRequest;
            else if (ex is NotFoundException)
                code = HttpStatusCode.NotFound;
            else if (ex is ForbiddenException)
                code = HttpStatusCode.Forbidden;

            var result = JsonConvert.SerializeObject(new { error = ex.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
