using Template.WebApi.Filters;
using Template.WebApi.Resolver;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Template.WebApi
{
	/// <summary>
	/// Define a inicialização para o signalR.
	/// </summary>
	public partial class Startup
	{
		/// <summary>
		/// Configura a aplicação para utilização de web api.
		/// </summary>
		/// <param name="config">A configuração http.</param>
		public void ConfigureWebApi(HttpConfiguration config)
		{
			//config.Filters.Add(new AuthorizeAttribute());
			config.Filters.Add(new ElmahErrorAttribute());

			var corsAttr = new EnableCorsAttribute("*", "*", "*");
			config.EnableCors(corsAttr);

			// map web api route prefix
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
			config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
		}
	}
}
