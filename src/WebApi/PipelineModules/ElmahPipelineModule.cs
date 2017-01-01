using Elmah;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Web;

namespace Template.WebApi.PipelineModules
{
	/// <summary>
	/// 
	/// </summary>
	public class ElmahPipelineModule : HubPipelineModule
	{
		/// <summary>
		/// Raises the error signal.
		/// </summary>
		/// <param name="exception">The exception.</param>
		/// <returns></returns>
		private static bool RaiseErrorSignal(Exception exception)
		{
			var context = HttpContext.Current;

			if (context == null)
				return false;

			try
			{
				var signal = ErrorSignal.FromCurrentContext();

				if (signal == null)
					return false;

				signal.Raise(exception, context);

				return true;
			}
			catch (ArgumentNullException)
			{
				return false;
			}
		}

		/// <summary>
		/// Logs the exception.
		/// </summary>
		/// <param name="exception">The exception.</param>
		/// <param name="invokerContext">The invoker context.</param>
		private static void LogException(Exception exception, IHubIncomingInvokerContext invokerContext)
		{
			var context = HttpContext.Current;

			ErrorLog errorLog = ErrorLog.GetDefault(context);

			errorLog.Log(new Error(exception));
		}

		/// <summary>
		/// This is called when an uncaught exception is thrown by a server-side hub method or the incoming component of a
		/// module added later to the <see cref="T:Microsoft.AspNet.SignalR.Hubs.IHubPipeline" />. Observing the exception using this method will not prevent
		/// it from bubbling up to other modules.
		/// </summary>
		/// <param name="exceptionContext">Represents the exception that was thrown during the server-side invocation.
		/// It is possible to change the error or set a result using this context.</param>
		/// <param name="invokerContext">A description of the server-side hub method invocation.</param>
		protected override void OnIncomingError(ExceptionContext exceptionContext, IHubIncomingInvokerContext invokerContext)
		{
			var context = HttpContext.Current;

			ErrorLog errorLog = ErrorLog.GetDefault(context);

			errorLog.Log(new Error(exceptionContext.Error));

			base.OnIncomingError(exceptionContext, invokerContext);
		}
	}
}