using System.Text;
using ConsumoDeAPIs;
using ConsumoDeAPIs.Integration.Interfaces;
using ConsumoDeAPIs.Integration.Response.Refit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Refit;
using TaskManager.Context;
using TaskManager.Interfaces;
using TaskManager.Model;
using TaskManager.Repository;
using TaskManager.Service;
using TasManager.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<TaskManagerContext> (
    options => options.UseNpgsql (
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();  

builder.Services.AddScoped<IViaCepIntegracao, ViaCepIntegracao>(); 

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services
    .AddRefitClient<IViaCepIntegracaoRefit>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://viacep.com.br/"))
;

// builder.Services.AddMvc(options => options.Filters.Add(typeof(IExceptionFilter)));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(swagger => 
{
    swagger.SwaggerDoc("v1", new OpenApiInfo 
    {
        Title = "TaskManager - API", 
        Version = "V1",
        Description = "API para o controle de Tarefas"
    });

    var SecuritySchema = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Entre o JWT Bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    swagger.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, SecuritySchema);

    swagger.AddSecurityRequirement
    (
        new OpenApiSecurityRequirement
        {
            { SecuritySchema, Array.Empty<string>() }
        }
    );
});



builder.Services.AddControllers()
    .AddNewtonsoftJson(options => {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    }
);

builder.Services.AddIdentity<UserIdentityApp, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 12;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
}).AddEntityFrameworkStores<TaskManagerContext>();


builder.Services.AddAuthentication(
    options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultForbidScheme = 
        options.DefaultScheme = 
        options.DefaultSignInScheme =
        options.DefaultSignOutScheme =  JwtBearerDefaults.AuthenticationScheme;
    }
).AddJwtBearer(
    options => {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,  
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience  = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
        };
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseHttpsRedirection();

app.Run();
