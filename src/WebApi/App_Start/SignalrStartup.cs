using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Owin;
using Template.WebApi.PipelineModules;
using Template.WebApi.Resolver;
using System;

namespace Template.WebApi
{
	/// <summary>
	/// Define a inicialização para o signalR.
	/// </summary>
	public partial class Startup
	{
		/// <summary>
		/// Factory para serialização no formato json.
		/// </summary>
		private static readonly Lazy<JsonSerializer> JsonSerializerFactory = new Lazy<JsonSerializer>(GetJsonSerializer);

		/// <summary>
		/// Factory method para json serializer.
		/// </summary>
		private static JsonSerializer GetJsonSerializer()
		{
			return new JsonSerializer
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver
				{
					AssembliesToInclude = { typeof(Startup).Assembly }
				}
			};
		}

		/// <summary>
		/// Configura a aplicação para utilização do SignalR.
		/// </summary>
		/// <param name="app">The application.</param>
		public void ConfigureSignalR(IAppBuilder app)
		{
			GlobalHost.HubPipeline.AddModule(new ElmahPipelineModule());
			GlobalHost.DependencyResolver.Register(typeof(JsonSerializer), () => JsonSerializerFactory.Value);

			//GlobalHost.DependencyResolver.UseRedis(
			//		AppSettingsExtensions.Get("HostRedis", "localhost"),
			//		AppSettingsExtensions.Get("PortRedis", 6379),
			//		AppSettingsExtensions.Get("PasswordRedis", string.Empty),
			//		AppSettingsExtensions.Get("KeyRedis", string.Empty)
			//);

			app.Map("/signalr", map =>
			{
				var hubConfiguration = new HubConfiguration { EnableDetailedErrors = true };

				map.UseCors(CorsOptions.AllowAll);
				map.RunSignalR(hubConfiguration);
			});
		}
	}
}
