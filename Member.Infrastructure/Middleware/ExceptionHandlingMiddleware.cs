using Microsoft.AspNetCore.Http;

namespace Member.Infrastructure.Middleware
{
	public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionHandlingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next.Invoke(context);
			}
			//catch(ValidationException ex)
			//{

			//}
			catch (Exception ex)
			{
				Handle(ex);
			}
		}

		private void Handle(Exception exception)
		{
			Console.WriteLine(exception.Message);
		}
	}
}

