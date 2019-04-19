using System.Web.Http;

namespace Rxn.WebApi.Controllers
{
    public class ApiControllerBase : ApiController
    {
        protected static Response Success()
        {
            return new Response
            {
                Success = true,
            };
        }

        protected static Response<T> Success<T>(T result)
        {
            return new Response<T>
            {
                Success = true,
                Result = result,
            };
        }

        protected static Response Failed()
        {
            return new Response
            {
                Success = false,
            };
        }

        protected static Response Failed(string message)
        {
            return new Response
            {
                Success = false,
                ErrorMessage = message
            };
        }
    }
}