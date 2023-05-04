using System;
using Member.Infrastructure.Middleware;

namespace Member.Web.Api.Extentions
{
	public static class ApplicationBuilderExtentions
	{
		public static void AddMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<ExceptionHandlingMiddleware>();
		}	
	}
}

