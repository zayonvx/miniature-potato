using Microsoft.OpenApi.Models;
using OnePlan.Core.Middleware;
using OnePlan.Data;
using OnePlan.Data.Context;
using OnePlan.Data.Dependencies;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ConfigurationManager configurationManager = builder.Configuration;

builder.Services.RegisterDependencies(configurationManager);

builder.Services.SetupAuth();
// Add services to the container.
builder.Services.AddControllers();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {Title = "OnePlan,Web", Version = "v1"});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnenPlan.Web v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


