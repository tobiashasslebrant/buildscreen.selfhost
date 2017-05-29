using System.Web.Http;
using Owin;

namespace Buildscreen.Selfhost.Scaffolding
{
    public class StartOwin
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{fileName}",
                defaults: new
                {
                    controller = "Static",
                    fileName = "index.html"
                }
            );

            appBuilder.UseWebApi(config);
        }
    }
}