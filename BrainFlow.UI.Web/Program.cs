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
builder.Services.AddAuthentication("BrainFlow.Cookie")
    .AddCookie("BrainFlow.Cookie", options =>
    {
        options.Cookie.Name = "BrainFlow.Auth";
        options.LoginPath = "/Conta/Login";
        options.LogoutPath = "/Conta/Logout";
        options.AccessDeniedPath = "/Conta/AcessoNegado"; 
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
    });

// Add Authorization Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("TipoUsuario", "3"));
    options.AddPolicy("AfiliadoOrAdmin", policy => policy.RequireClaim("TipoUsuario", "2", "3"));
    options.AddPolicy("UsuarioLogado", policy => policy.RequireAuthenticatedUser());
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

// Middleware personalizado para informações do usuário
app.UseMiddleware<BrainFlow.UI.Web.Middleware.UserInfoMiddleware>();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Conta}/{action=Login}/{id?}");

app.Run();
