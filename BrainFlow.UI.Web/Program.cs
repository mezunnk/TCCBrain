using BrainFlow.Data.Common;
using BrainFlow.Repository.Context;
using BrainFlow.UI.Web;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add Connection String
var connectionString = builder.Configuration.GetConnectionString("BrainFlowConnection");

// Add DbContext
builder.Services.AddDbContext<BrainFlowContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add e-mail settings
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

// Add DependeyContainer
DependencyContainer.RegisterContainers(builder.Services);

// Add Cookie Authentication
builder.Services.AddAuthentication("Identity.Login")
    .AddCookie("Identity.Login", options =>
    {
        options.Cookie.Name = "BrainFlow.Login";
        options.LoginPath = "/Conta/Login";
        options.AccessDeniedPath = "/Conta/AcessoNegado"; 
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
    });

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Conta}/{action=Login}/{id?}");

app.Run();
