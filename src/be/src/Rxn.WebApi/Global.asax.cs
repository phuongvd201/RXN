using System.Web;
using System.Web.Http;

namespace Rxn.WebApi
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            ContainerConfig.Register();
            WebApiConfig.Register(GlobalConfiguration.Configuration);

            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}