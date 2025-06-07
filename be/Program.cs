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
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetSection("ConnectionStrings").GetSection("Admin").Value;

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
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWTSecret:Key").Value!)),
        RoleClaimType = "roleId",
    };

});
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();
});

builder.Services.AddScoped<IRepository<Achievement>, AchievementRepository>();
builder.Services.AddScoped<IRepository<AchievementItem>, AchievementItemRepository>();
builder.Services.AddScoped<IRepository<Criteria>, CriteriaRepository>();
builder.Services.AddScoped<IRepository<Role>, RoleRepository>();
builder.Services.AddScoped<IRepository<Department>, DepartmentRepository>();
builder.Services.AddScoped<IRepository<Employee>, EmployeeRepository>();
builder.Services.AddScoped<IRepository<EmployeeDetail>, EmployeeDetailRepository>();
builder.Services.AddScoped<IEvaluationScheduleRepository, EvaluationScheduleRepository>();
builder.Services.AddScoped<IEvaluationScoreRepository, EvaluationScoreRepository>();
builder.Services.AddScoped<IRepository<Evidence>, EvidenceRepository>();
builder.Services.AddScoped<IRepository<Grade>, GradeRepository>();
builder.Services.AddScoped<IRepository<Group>, GroupRepository>();
builder.Services.AddScoped<IRepository<Image>, ImageRepository>();
builder.Services.AddScoped<IRepository<Operation>, OperationRepository>();
builder.Services.AddScoped<IRepository<PerformanceEvaluation>, PerformanceEvaluationRepository>();
builder.Services.AddScoped<IRepository<Plant>, PlantRepository>();
builder.Services.AddScoped<IRepository<PositionE>, PositionERepository>();
builder.Services.AddScoped<IRepository<Process>, ProcessRepository>();
builder.Services.AddScoped<IRepository<ValidationToken>, ValidationTokenRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRepository<WorkingDetail>, WorkingDetailRepository>();

builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddSingleton<ISocketManager, SocketManager>();

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

app.UseCors("AppPolicy");

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
    RequestPath = "/static"
});

var webSocketConfig = new WebSocketOptions();
webSocketConfig.AllowedOrigins.Add("http://localhost:5173");
app.UseWebSockets(webSocketConfig);

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
