using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Template.WebApi.Authorization;
using System;

namespace Template.WebApi
{
	/// <summary>
	/// Define a inicialização para autenticação oauth.
	/// </summary>
	public partial class Startup
	{
		/// <summary>
		/// Configura a aplicação para utilização do OAuth.
		/// </summary>
		/// <param name="app">The application.</param>
		public void ConfigureOAuth(IAppBuilder app)
		{
			OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
			{
				AllowInsecureHttp = true,
				TokenEndpointPath = new PathString("/token"),
				AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
				Provider = new AuthorizationServerProvider()
			};

			// Token Generation
			app.UseOAuthAuthorizationServer(OAuthServerOptions);
			app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
		}
	}
}
