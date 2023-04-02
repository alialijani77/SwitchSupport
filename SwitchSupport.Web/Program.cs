#region Services
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SwitchSupport.DataLayer.Context;
using SwitchSupport.Domain.ViewModels.Common;
using SwitchSupport.IoC;
using SwitchSupport.Web.Hubs;
using WebMarkupMin.AspNetCore7;

var builder = WebApplication.CreateBuilder(args);

#region DbContext
builder.Services.AddDbContext<SwitchSupportDbContext>(options => options.
UseSqlServer(builder.Configuration.GetConnectionString("SwitchSupportDbContext")));
#endregion

#region RegisterDependencies
DependencyContainer.RegisterDependencies(builder.Services);
#endregion

#region Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
});

#endregion

#region Minify
//builder.Services.AddControllersWithViews();

//builder.Services.AddWebMarkupMin(
//    options =>
//    {
//        options.AllowMinificationInDevelopmentEnvironment = true;
//        options.AllowCompressionInDevelopmentEnvironment = true;
//    })
//    .AddHtmlMinification(
//        options =>
//        {
//            options.MinificationSettings.RemoveRedundantAttributes = true;
//            options.MinificationSettings.RemoveHttpProtocolFromAttributes = true;
//            options.MinificationSettings.RemoveHttpsProtocolFromAttributes = true;
//        })
//    .AddHttpCompression();
#endregion

#endregion

// Add services to the container.
#region MiddleWare
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.Configure<ScoreManagementViewModel>(builder.Configuration.GetSection("ScoreManagement"));
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
//app.UseWebMarkupMin();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseStatusCodePagesWithRedirects("/{0}");
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapHub<OnlineUsersHub>("/hubs/online-users");
app.Run();
#endregion
