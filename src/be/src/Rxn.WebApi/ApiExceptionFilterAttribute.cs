using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Rxn.WebApi
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            HttpStatusCode httpStatusCode;
            if (context.Exception is HttpException)
            {
                var httpException = (HttpException)context.Exception;
                httpStatusCode = (HttpStatusCode)httpException.GetHttpCode();
            }
            else
            {
                httpStatusCode = GetStatusCode(context);
            }

            context.Response = context.Request.CreateResponse(
                httpStatusCode,
                new Response
                {
                    Success = false,
                    ErrorMessage = context.Exception.Message,
                }
            );
        }

        protected virtual HttpStatusCode GetStatusCode(HttpActionExecutedContext context)
        {
            if (context.Exception is ArgumentException)
            {
                return HttpStatusCode.BadRequest;
            }

            return HttpStatusCode.InternalServerError;
        }
    }
}