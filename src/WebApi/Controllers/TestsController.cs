using System.Web.Http;

namespace Template.WebApi.Controllers
{
	/// <summary>
	/// Define uma controller de api para tests.
	/// </summary>
	[RoutePrefix("api/tests")]
	public class TestsController : ApiController
	{
		/// <summary>
		/// Obtém um string de test da api.
		/// </summary>
		/// <returns>string</returns>
		[Route()]
		public string Get()
		{
			return "Ok";
		}
	}
}
