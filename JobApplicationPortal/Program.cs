

using JobApplicationPortal.Repo;
using JobApplicationPortal.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://0.0.0.0:5212");


// Add services to the container
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<JobRepo>();
builder.Services.AddScoped<StudentRepo>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IEmployerService, EmployerService>();
builder.Services.AddScoped<EmployerRepo>();
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Remove(new AutoValidateAntiforgeryTokenAttribute());
});

//builder.Services.AddAntiforgery(options => options.SuppressXFrameOptionsHeader = true);
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "RequestVerificationToken"; // Ensure this header is used
});

// transfer data over memory cache with a time limit
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AspNetCore.Session";  // Use a specific cookie name
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Necessary for GDPR compliance
});

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

// Middleware pipeline configuration
app.UseStaticFiles(); // Serve static files like CSS, JS, etc.

app.UseRouting();

// uses the implemented session state
app.UseSession();
app.UseCors("AllowAll");
//// Middleware to handle OPTIONS requests globally
app.Use(async (context, next) =>
{
    if (context.Request.Method == HttpMethods.Options)
    {
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, PATCH, DELETE, OPTIONS");
        context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
        context.Response.StatusCode = StatusCodes.Status204NoContent;
        return;
    }
    await next();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Default route

app.Run();
