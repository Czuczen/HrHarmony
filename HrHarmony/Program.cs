using HrHarmony.Configuration.Dependencies;
using HrHarmony.Configuration.Secrets;
using HrHarmony.Consts;
using HrHarmony.Data.Database;
using HrHarmony.Logging;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Ustawienie kultury polskiej dla formatowania liczb i dat.
var cultureInfo = new CultureInfo("pl-PL");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// Dodaj logowanie do pliku
builder.AddFileLogger();

// Dodaj kontekst bazy danych
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = SecretsProvider.GetConnectionString("HrHarmony", DbConnectionTypes.DefaultConnection);
    options.UseSqlServer(connectionString);
});

// Rejestracja zależności według konwencji
builder.Services.RegisterDependenciesByConvention();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.Run();
