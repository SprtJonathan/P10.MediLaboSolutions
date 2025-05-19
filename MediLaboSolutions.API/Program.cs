using MediLaboSolutions.API.Data;
using MediLaboSolutions.API.Repositories;
using MediLaboSolutions.Common.Utils;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();

// Chargement des fichiers appsettings.json (developpement ou normal)
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("La chaîne de connexion n'est pas configurée.");
}

builder.Services.AddDbContext<MediLaboSolutionsDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAuthentication("Identity.Application")
    .AddCookie("Identity.Application");
builder.Services.AddAuthorization();

var cert = CertificateHelper.GetCertificateByThumbprint("61EAC67F1199C602BBD1B2734F2D260510650F1D");

builder.Services.AddDataProtection()
    .ProtectKeysWithCertificate(cert)
    .SetApplicationName("MediLabo");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/health", () => Results.Ok("Healthy"));

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
