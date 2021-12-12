using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sudoku.Common;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Sudoku.ExceptionHandling
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            //httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError; 
            var response = new AppResponse(null, ex.Message, httpContext.Response.StatusCode);

            var result = JsonConvert.SerializeObject(response, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            });
            httpContext.Response.ContentType = "application/json";

            return httpContext.Response.WriteAsync(result);
        }
    }
}
