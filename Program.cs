using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using registration.Entities;
using registration.Interfaces;
using registration.Services.AccountService;
using registration.Services.AccountService.UserRegisterService;
using registration.Services.AccountServices.PasswordHasherService;
using registration.Services.AccountServices.PasswordRecoveryService;
using registration.Services.AccountServices.UserLoginService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // مدت زمان انقضای سشن
    options.Cookie.HttpOnly = true; // امنیت بیشتر برای کوکی
    options.Cookie.IsEssential = true; // برای جلوگیری از حذف شدن توسط GDPR
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

builder.Services.AddDbContext<ApplicationDbcontext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


builder.Services.AddScoped<IUserRegistrationService, UserRegistrationService>();
builder.Services.AddScoped<IUserPasswordService, UserPasswordService>(); // ثبت UserPasswordService
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
builder.Services.AddScoped<LoginValidationService>();
builder.Services.AddScoped<IComfirmPasswordService, ResetPasswordService>();
builder.Services.AddScoped<IPremiumActivationService, PremiumActivationService>();
builder.Services.AddScoped<CookieCreator>();
builder.Services.AddScoped<RegisterVlidation>();

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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
