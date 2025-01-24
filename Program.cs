using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using registration.AccountService;
using registration.Entities;
using registration.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

builder.Services.AddDbContext<ApplicationDbcontext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


builder.Services.AddScoped<IUserRegistrationService, UserRegistrationService>();
builder.Services.AddScoped<IUserPasswordService, UserPasswordService>(); // ثبت UserPasswordService
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
builder.Services.AddScoped<UserLoginService>();
// افزودن خدمات به کانتینر.
builder.Services.AddControllersWithViews();


var app = builder.Build();

// تنظیم خط لوله درخواست HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
