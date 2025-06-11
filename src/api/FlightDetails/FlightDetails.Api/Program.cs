using FlightDetails.Api.Hubs;
using FlightDetails.Api.Interfaces;
using FlightDetails.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddHttpClient();

// Add SignalR with the connection to AzureSignalR
builder
    .Services
    .AddSignalR();
//    .AddAzureSignalR();

// Add Open CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            //.AllowAnyOrigin()
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            ;
    });
});

// Register AeroDataBoxService with API key from configuration
builder.Services.AddScoped<IFlightUpdateService>(serviceProvider =>
{
    var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var apiKey = configuration["AeroDataBox:ApiKey"]; // Adjust the key path as needed
    
    return new AeroDataBoxService(httpClientFactory, apiKey);
});

// Register the flight group tracker service
builder.Services.AddSingleton<IFlightConnectionTracker, FlightConnectionTracker>();

// Register the background service
builder.Services.AddHostedService<FlightUpdateWorker>();

var app = builder.Build();

app.UseCors();
app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.MapControllers();
app.MapHub<FlightHub>("flight-hub");

app.UseHttpsRedirection();

app.Run();