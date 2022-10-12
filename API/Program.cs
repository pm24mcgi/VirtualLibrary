using API.Config;
using API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
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

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
builder.Services
    .AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(
                    builder.Configuration["Authentication:SecretKey"]))
        };
    });

builder.Services.AddScoped<IMigrateService, MigrateService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ISeedingService, SeedingService>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SupportNonNullableReferenceTypes();
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Virtual Library API", Version = "v1" });
    // Telling Swagger the type of protection wanted for the API
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Auth header using Bearer scheme.",
        Name = "Auth",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    // We could add a global security requirement like this, makes no sense for this project
    //options.AddSecurityRequirement(new OpenApiSecurityRequirement
    //{
    //    {
    //        new OpenApiSecurityScheme
    //        {
    //            Reference = new OpenApiReference
    //            {
    //                Type=ReferenceType.SecurityScheme,
    //                Id="Bearer"
    //            }
    //        },
    //        new string[]{}
    //    }
    //});
});

var app = builder.Build();

await app.MigrateDatabasesAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.SeedDataAsync();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();