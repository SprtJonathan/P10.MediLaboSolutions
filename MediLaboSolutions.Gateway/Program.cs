using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("configuration.json", optional: false, reloadOnChange: true);

builder.Services
    .AddControllers()
    .AddApplicationPart(typeof(MediLaboSolutions.Gateway.Controllers.StatusController).Assembly);

builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseOcelot().Wait();

await app.RunAsync();
