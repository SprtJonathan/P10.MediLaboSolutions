using MediLaboSolutions.Web.Data;
using MediLaboSolutions.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory()
});

// Ajout des variables d'environnement comme source de config
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

// Ajout des services personnalis�s
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<NoteService>();
builder.Services.AddScoped<AssessmentService>();
builder.Services.AddScoped<JwtTokenService>();
builder.Services.AddTransient<JwtTokenHandler>();
builder.Services.AddHttpContextAccessor();

// Configurer HttpClient avec le JwtTokenHandler pour les appels aux microservices
builder.Services.AddHttpClient<PatientService>(client =>
{
    client.BaseAddress = new Uri("http://medilabosolutions.gateway/"); // Pointe vers le Gateway Ocelot
}).AddHttpMessageHandler<JwtTokenHandler>();

builder.Services.AddHttpClient<NoteService>(client =>
{
    client.BaseAddress = new Uri("http://medilabosolutions.gateway/"); // Pointe vers le Gateway Ocelot
}).AddHttpMessageHandler<JwtTokenHandler>();

builder.Services.AddHttpClient<AssessmentService>(client =>
{
    client.BaseAddress = new Uri("http://medilabosolutions.gateway/"); // Pointe vers le Gateway Ocelot
}).AddHttpMessageHandler<JwtTokenHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();