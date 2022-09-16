// Assuming these are all imports?
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using VirtualLibrary.Data;

// Builder has configuration, logging, and many other services added to the DI container
var builder = WebApplication.CreateBuilder(args);

//Establishes the connection string pulled from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add SERVICES to the container.
// Services are typically resolved from DI using constructor injection. The DI framework provides an instance of this service at runtime.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});
// These services are resolved from DI using constructor injection. The DI framework provides an instance of this service at runtime.
// Constructor injection seems to be contained in all 'public class IndexModel : PageModel' -> index.cshtml.cs files

// HOST
// (3) -> .NET WebApplication Host, also known as the Minimal Host.
// The .NET WebApplication Host is recommended and used in all the ASP.NET Core templates.
var app = builder.Build();
// The WebApplicationBuilder.Build method configures a host with a set of default options, such as:
// 1) Use Kestrel as the web server and enable IIS integration.
// 2) Load configuration from appsettings.json, environment variables, command line arguments, and other configuration sources.
// 3) Send logging output to the console and debug providers.
// --- Kestrel is a cross-platform web server. Kestrel is often run in a reverse proxy configuration using IIS. In ASP.NET Core 2.0 or later, Kestrel can be run as a public-facing edge server exposed directly to the Internet.
// --- The server surfaces requests to the app as a set of request features composed into an HttpContext.

// -------------
// CONFIGURATION
// -------------
// Built-in configuration providers are available for a variety of sources, such as .json files, .xml files, environment variables, and command-line arguments.
// By default, ASP.NET Core apps are configured to read from appsettings.json, environment variables, the command line, and more.
// When the app's configuration is loaded, values from environment variables override values from appsettings.json.

// ENVIRONMENT
// Specify the environment an app is running in by setting the ASPNETCORE_ENVIRONMENT environment variable.
// ASP.NET Core reads that environment variable at app startup and stores the value in an IWebHostEnvironment implementation.
// This implementation is available anywhere in an app via dependency injection (DI).
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // Configures the exception handler and HTTP Strict Transport Security Protocol (HSTS) middleware when not running in the Development environment.
    // Informs browsers that the site should only be accessed using HTTPS, and that any future attempts to access it using HTTP should automatically be converted to HTTPS.
    // HTTPS uses TLS (SSL) to encrypt normal HTTP requests and responses, and to digitally sign those requests and responses.
    app.UseHsts();
}

// Middleware
// Each component performs operations on an HttpContext and either invokes the next middleware in the pipeline or terminates the request.
app.UseHttpsRedirection();
app.UseStaticFiles();

// ROUTING
// URL pattern that is mapped to a handler.

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();


app.Run();

// ------------
// Content Root
// ------------
// Razor files (.cshtml, .razor)
// Configuration files (.json, .xml)
// Data files (.db)

// --------
// Web Root
// --------
// In the wwwroot folder, base path for public, static resource files
// Stylesheets (.css)
// JavaScript (.js)
// Images (.png, .jpg)

