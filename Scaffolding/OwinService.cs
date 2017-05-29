using System;
using System.Configuration;
using Microsoft.Owin.Hosting;

namespace Buildscreen.Selfhost.Scaffolding
{
    public class OwinService
    {
        private IDisposable _webApp;

        public void Start()
        {
            var url = ConfigurationManager.AppSettings["buildscreen-url"];
            _webApp = WebApp.Start<StartOwin>(url);
        }

        public void Stop()
        {
            _webApp.Dispose();
        }
    }
}