using Microsoft.EntityFrameworkCore;
using CollectionsApp.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using CollectionsApp.Services;
using CollectionsApp.Data;
//using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AZURE_SQL_IDENTITY_CONNECTIONSTRING") ?? throw new InvalidOperationException("Connection string 'CollectionsAppIdentityDbContextConnection' not found.");
builder.Services.AddDbContext<CollectionsAppIdentityDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<CollectionsAppIdentityDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.AddScoped<IEmailSender, ConsoleEmailSender>();

builder.Services.AddDbContext<CollectionsAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//app.UseDeveloperExceptionPage();
//app.UseDatabaseErrorPage();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();;
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

IdentitySeedData.EnsurePopulated(app);

app.Run();

