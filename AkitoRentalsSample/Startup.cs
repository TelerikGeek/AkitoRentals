using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Newtonsoft.Json;
using System.Web.Http;
using Owin;

[assembly: OwinStartupAttribute(typeof(AkitoRentalsSample.Startup))]
namespace AkitoRentalsSample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Re‌​ferenceLoopHandling = ReferenceLoopHandling.Ignore;

            ConfigureAuth (app);
        }
    }
}
