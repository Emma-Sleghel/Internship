using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserOperation.Data;
using UserOperation.Data.Entities;
using UserOperation.Data.Repository;
using UserOperation.Services.Helpers;
using UserOperation.Services.Services;
using UserOperation.Web.Core;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection"); ;
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(connectionString)); ;
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>(); ;


builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<ILeaveService, LeaveService>();
builder.Services.AddScoped<IStabilityService, StabilityService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IStabilityHelper, StabilityHelper>();
#region Authorization
AddAuthorizationPolicies();
#endregion

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
app.UseAuthentication(); ;
app.UseAuthorization();
app.MapControllerRoute(
name: "default",
pattern: "{controller=Leave}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();

void AddAuthorizationPolicies()
{
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy(Constants.Policies.RequireAdmin, policy => policy.RequireRole(Constants.Roles.Administrator));
        options.AddPolicy(Constants.Policies.RequireManager, policy => policy.RequireRole(Constants.Roles.Manager));
    });
}