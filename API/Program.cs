using API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using VL.Shared.Data;
using VL.Shared.Interfaces;
using VL.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// container
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IMigrateService, MigrateService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ISeedingService, SeedingService>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "JWT Auth header using Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();

    var info = new OpenApiInfo()
    {
        Version = "1",
        Title = "API",
        Description = "Virtual Library API",
    };

    options.SwaggerDoc("v1", info);
});

// Add authentication scheme
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8
                    .GetBytes(builder.Configuration["Authentication:SecretKey"])),
            ValidAudience = builder.Configuration["Authentication:Audience"]
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IsLibrarian", policy =>
        policy.RequireRole("Librarian"));

    options.AddPolicy("IsAllUsers", policy =>
        policy.RequireRole("User", "Librarian"));
});

var app = builder.Build();

await app.MigrateDatabasesAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.SeedDataAsync();
}

app.UseHttpsRedirection();

// Authorization Middleware
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();