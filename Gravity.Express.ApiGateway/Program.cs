using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks(); 
builder.Services.AddOcelot();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("CorsPolicy");
app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health");
});

app.UseOcelot();

app.Run();