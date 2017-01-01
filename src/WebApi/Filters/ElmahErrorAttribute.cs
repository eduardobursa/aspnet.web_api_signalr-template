using System.Web.Http.Filters;

namespace Template.WebApi.Filters
{
	/// <summary>
	/// Define um filtro de exceções, para log das mesmas usando elmah.
	/// </summary>
	public class ElmahErrorAttribute : ExceptionFilterAttribute
	{
		/// <summary>
		/// Raises the exception event.
		/// </summary>
		/// <param name="actionExecutedContext">The context for the action.</param>
		public override void OnException(HttpActionExecutedContext actionExecutedContext)
		{
			if (actionExecutedContext.Exception != null)
				Elmah.ErrorSignal.FromCurrentContext().Raise(actionExecutedContext.Exception);

			base.OnException(actionExecutedContext);
		}
	}
}