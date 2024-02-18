using HaircutAppAPI.Security;
using HaircutAppAPI.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Cosmos DB
var cosmosConnectionString = builder.Configuration["CosmosDb:AccountEndpoint"];
var databaseName = "HaircutAppDB";
var containerName = "Reservations";
builder.Services.AddSingleton<CosmosDbContext>(s => new CosmosDbContext(cosmosConnectionString, databaseName, containerName));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

	// Define the OAuth2.0 scheme that's being used (i.e., bearer token, JWT)
	c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
	{
		Description = "API Key needed to access the endpoints. X-API-Key: YOUR_API_KEY",
		In = ParameterLocation.Header,
		Name = "X-API-Key", // Name of the header to be used
		Type = SecuritySchemeType.ApiKey,
		Scheme = "ApiKeyScheme"
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement
		{
			{
				new OpenApiSecurityScheme
				{
					Reference = new OpenApiReference
					{
						Type = ReferenceType.SecurityScheme,
						Id = "ApiKey"
					},
					Scheme = "ApiKeyScheme",
					Name = "ApiKey",
					In = ParameterLocation.Header,
				},
				Array.Empty<string>()
			}
		});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
		// To serve Swagger UI at application's root page, set the RoutePrefix property to an empty string
		c.RoutePrefix = string.Empty;
	});
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ApiKeyAuthenticationMiddleware>();

app.MapControllers();

app.Run();
