using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PivotalImporter2.Domain.StructureMap;

namespace PivotalImporter2
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			AuthConfig.RegisterAuth();

			BootStrapper.Bootstrap();
			AreaRegistration.RegisterAllAreas();
			//RegisterRoutes(RouteTable.Routes);
			InitializeContainer();

			// Json deserialiser in mvc 2 futures nuget package
			ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());
		}
		/*
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);

		}
		*/
		private void InitializeContainer()
		{
			ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
		}
	}
}