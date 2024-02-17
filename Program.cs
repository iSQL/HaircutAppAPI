using HaircutAppAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Cosmos DB
var cosmosConnectionString = builder.Configuration["ConnectionStrings:CosmosDb"];
var databaseName = "HaircutAppointmentsDB";
var containerName = "Appointments";
builder.Services.AddSingleton<CosmosDbContext>(s => new CosmosDbContext(cosmosConnectionString, databaseName, containerName));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
