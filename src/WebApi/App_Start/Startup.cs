using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(Template.WebApi.Startup))]

namespace Template.WebApi
{
	/// <summary>
	/// Define um ponto de entrada para a configuração da aplcação.
	/// </summary>
	public partial class Startup
	{
		/// <summary>
		/// Executa a configuração da aplicação.
		/// </summary>
		/// <remarks>
		/// For more information on how to configure 
		/// your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
		/// </remarks>
		/// <param name="app">A aplicação.</param>
		public void Configuration(IAppBuilder app)
		{
			var config = GlobalConfiguration.Configuration;

			ConfigureOAuth(app);
			ConfigureSignalR(app);
			ConfigureSwagger(config);
			ConfigureWebApi(config);

			app.UseCors(CorsOptions.AllowAll);
			app.UseWebApi(config);

			config.EnsureInitialized();
		}
	}
}
