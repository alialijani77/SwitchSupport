#region Services
using Microsoft.EntityFrameworkCore;
using SwitchSupport.DataLayer.Context;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<SwitchSupportDbContext>(options => options.
UseSqlServer(builder.Configuration.GetConnectionString("SwitchSupportDbContext")));
#endregion

// Add services to the container.
#region MiddleWare
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
#endregion
