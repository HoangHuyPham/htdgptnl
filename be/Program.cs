using be.Contexts;
using be.Controllers;
using be.Models;
using be.Repos;
using be.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.FileProviders;
using be.Services;
using be.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetSection("ConnectionStrings").GetSection("admin").Value;
// Add services to the container.

builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: "AppPolicy", policy =>
            {
                policy.WithOrigins("http://localhost:5173").AllowAnyHeader()
                                                  .AllowAnyMethod();
            });
        });
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWTSecret:Key").Value!)),
        RoleClaimType = "roleId",
    };
});
builder.Services.AddAuthorization(options=>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();
});

builder.Services.AddScoped<IRepository<EvaluationSchedule>, EvaluationScheduleRepository>();
builder.Services.AddScoped<IRepository<PerformanceEvaluation>, PerformanceEvaluationRepository>();
builder.Services.AddScoped<IRepository<Achievement>, AchievementRepository>();
builder.Services.AddScoped<IRepository<AchievementItem>, AchievementItemRepository>();
builder.Services.AddScoped<IRepository<Image>, ImageRepository>();
builder.Services.AddScoped<IRepository<Role>, RoleRepository>();
builder.Services.AddScoped<IRepository<ProofImage>, ProofImageRepository>();
builder.Services.AddScoped<IRepository<EvaluateScore>, EvaluateScoreRepository>();
builder.Services.AddScoped<IRepository<Criteria>, CriteriaRepository>();
builder.Services.AddScoped<IRoleEvaluationScheduleRepository, RoleEvaluationScheduleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid JWT Bearer token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "swagger");
        options.RoutePrefix = string.Empty;
    });
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
    RequestPath = "/static"
});

app.UseCors("AppPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
