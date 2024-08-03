using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tawuniya.Data;
using Tawuniya.Services.Common;
using Tawuniya.Services.Departments;
using Tawuniya.Services.Employees;
using Tawuniya.Services.Layouts;
using Tawuniya.Services.Seats;
using Tawuniya.Services.Security;
using Tawuniya.Services.Users;
using Tawuniya.Web.Factories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("ConnectionString") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<TawuniyaDBContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddScoped<IDbContext, TawuniyaDBContext>();
//builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEncryptionService, EncryptionService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ISeatService, SeatService>();
builder.Services.AddScoped<ILayoutService, LayoutService>();
builder.Services.AddScoped<IUserModelFactory, UserModelFactory>();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.TryAddSingleton<ICommonAPIService, CommonAPIService>();
builder.Services.TryAddSingleton<IEmployeeModelFactory, EmployeeModelFactory>();

builder.Services.AddControllers();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Authentication";
    option.DefaultScheme = "Authentication";
    option.DefaultSignInScheme = "Authentication";
}).AddCookie("Authentication", options =>
{
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/Users/login";
});

builder.Services.AddAuthorization();

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
