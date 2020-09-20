using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace PatientManagement.API.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;

            string message = string.Empty;
            Type exceptionType = context.Exception.GetType();

            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                message = "Access to the Web API is not authorized.";
                status = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(ArgumentOutOfRangeException))
            {
                message = "value of an argument is outside the range of valid values";
                status = HttpStatusCode.InternalServerError;
            }
            else if (exceptionType == typeof(ArgumentException))
            {
                message = "non-null argument that is passed to a method is invalid";
                status = HttpStatusCode.InternalServerError;
            }
            else if (exceptionType == typeof(NullReferenceException))
            {
                message = "program access members of null object";
                status = HttpStatusCode.InternalServerError;
            }
            else
            {
                message = "Not found.";
                status = HttpStatusCode.NotFound;
            }
            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";

            string result = JsonConvert.SerializeObject(
                new
                {
                    message = message,
                    isError = true,
                    errorCode = status,
                    action = context.ActionDescriptor.DisplayName
                }); ;

            response.WriteAsync(result);
        }
    }
}
