using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore; // SQL Server için gerekli
using MyrtilleRDPManager.Data;       // DatabaseModels ve AppDbContext için
using MyrtilleRDPManager.Services;   // EncryptionService için

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// --- BÝZÝM EKLEDÝÐÝMÝZ SERVÝSLER ---

// 1. Encryption (Þifreleme) Servisi
// Uygulama boyunca tek bir instance yeterli (Singleton)
builder.Services.AddSingleton<EncryptionService>();

// 2. Veritabaný Baðlantýsý (SQL Server)
// appsettings.json dosyasýndaki "DefaultConnection" ismini kullanýr
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// -----------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();