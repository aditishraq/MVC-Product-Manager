using System.Web.Http;

namespace MvcApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Enable attribute routing (optional but recommended)
            config.MapHttpAttributeRoutes();

            // Configure a default route (if you prefer convention-based routing)
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Other configuration settings (e.g., formatters, message handlers) can be added here.
        }
    }
}
