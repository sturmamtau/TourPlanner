using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TourPlanner.BL;
using TourPlanner.DAL;
using TourPlanner.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularCorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // URL Angular Frontends
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// db connection
builder.Services.AddDbContext<TourPlannerContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// add to autmatically handle dependency injection
builder.Services.AddScoped<ITourRepository, TourRepository>();
builder.Services.AddScoped<ITourService, TourService>();
builder.Services.AddSingleton<TourMock>();
builder.Services.AddSingleton<TourLogMock>();
builder.Services.AddHttpClient<IRouteService, RouteService>();

builder.Services.AddScoped<ITourLogRepository, TourLogRepository>();
builder.Services.AddScoped<ITourLogService, TourLogService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseCors(policy => policy
    .WithOrigins("http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseCors("AllowAngular");
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
