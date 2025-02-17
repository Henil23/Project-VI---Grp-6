//using Microsoft.AspNetCore.Builder;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllersWithViews();

//// Add MongoDB Context as a Singleton
//builder.Services.AddSingleton<MongoDbContext>();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();

using JobApplicationPortal.Repo;
using JobApplicationPortal.Services;

var builder = WebApplication.CreateBuilder(args);

// running on the broadcast channel for testing
builder.WebHost.UseUrls("http://0.0.0.0:5212");

// Add services to the container
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<JobRepo>();
builder.Services.AddScoped<StudentRepo>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddControllersWithViews();

builder.Services.AddControllers().AddNewtonsoftJson(); // json formatting functionality

// transfer data over memory cache with a time limit
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Necessary for GDPR compliance
});

var app = builder.Build();

// Middleware pipeline configuration
app.UseStaticFiles(); // Serve static files like CSS, JS, etc.

app.UseRouting();

// uses the implemented session state
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Default route

app.Run();
