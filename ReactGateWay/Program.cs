using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.AspNetCore.Cors;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration.AddJsonFile("Routes.json",optional:false,reloadOnChange:true);
builder.Services.AddCors();
builder.Services.AddOcelot();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

// Configure CORS to allow requests from your React app
app.UseCors(builder => builder
    .WithOrigins("http://localhost:3000", "http://192.168.1.102:3000")
    .AllowAnyHeader()
    .AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

await app.UseOcelot();
    
app.Run();
