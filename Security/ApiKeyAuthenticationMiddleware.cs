namespace HaircutAppAPI.Security
{
	public class ApiKeyAuthenticationMiddleware
	{
		private readonly RequestDelegate _next;
		private const string ApiKeyHeaderName = "X-API-Key";

		public ApiKeyAuthenticationMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
			{
				context.Response.StatusCode = 401; // Unauthorized
				await context.Response.WriteAsync("API Key is missing");
				return;
			}

			var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
			var apiKey = appSettings.GetValue<string>("ApiKey");

			if (!apiKey.Equals(extractedApiKey))
			{
				context.Response.StatusCode = 401; // Unauthorized
				await context.Response.WriteAsync("Unauthorized client");
				return;
			}

			await _next(context);
		}
	}
}
