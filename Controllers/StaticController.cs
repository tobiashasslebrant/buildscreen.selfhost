using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace Buildscreen.Selfhost.Controllers
{
    public class StaticController : ApiController
    {
        public HttpResponseMessage Get(string fileName) => new HttpResponseMessage
        {
            Content = new StringContent(File.ReadAllText("static\\" + fileName, Encoding.UTF8),
                Encoding.UTF8, $"text/{Regex.Match(fileName,"\\.(.+?$)").Groups[1].Value}")
        };
    }
}