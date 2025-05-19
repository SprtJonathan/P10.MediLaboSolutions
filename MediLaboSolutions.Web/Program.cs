using MediLaboSolutions.Common.Utils;
using MediLaboSolutions.Web.Data;
using MediLaboSolutions.Web.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<NoteService>();

// Configurer HttpClient pour appeler l'API
builder.Services.AddHttpClient<PatientService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7157/"); // Pointe vers le Gateway
});
builder.Services.AddHttpClient<NoteService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7157/"); // Pointe vers le Gateway
});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});
builder.Services.AddHttpContextAccessor();

var cert = CertificateHelper.GetCertificateByThumbprint("61EAC67F1199C602BBD1B2734F2D260510650F1D");

builder.Services.AddDataProtection()
    .ProtectKeysWithCertificate(cert)
    .SetApplicationName("MediLabo");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
