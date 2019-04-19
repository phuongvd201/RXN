using System.Linq;
using System.Web.Http;

using Rxn.Common;
using Rxn.WebApi.Helpers;

namespace Rxn.WebApi.Controllers
{
    public class DataController : ApiControllerBase
    {
        [HttpGet]
        public Response PrePackTypes()
        {
            return Success(TempData.PrePackTypes.Select(x => x.ToDictionaryItem()).ToArray());
        }
    }
}