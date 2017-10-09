using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Buildscreen.Selfhost.Services;

namespace Buildscreen.Selfhost.Controllers
{
    public class BuildsController : ApiController
    {
        public HttpResponseMessage Get()
        {
            try
            {
                var buildService = new BuildService();
                var builds = buildService.GetBuilds().ToArray();
                var columns = string.Concat(builds.Select(s => $@"
                    <div class=""build-wrapper {s.Status}"">
                        <div class=""build"">
                            <a class=""nostyle"" href=""{s.WebUrl}""><h2>{s.BuildConfig.Project.Name}: {s.BuildConfig.Name}</h2></a>
                        </div>
                    </div>"));

                return new HttpResponseMessage()
                {
                    Content = new StringContent($@"
                    <div class=""columns"">
                        {columns}
                    </div>", Encoding.UTF8, "text/html")
                };
            }
            catch (Exception)
            {
                return new HttpResponseMessage()
                {
                    Content = new StringContent($@"
                    <div class=""failbox blink"">
			            <h2>Can not access TeamCity</h2>
		            </div>", Encoding.UTF8, "text/html")
                };
            }
        }

    }
}