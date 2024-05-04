using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TaskManager.Context;
using TaskManager.Interface;
using TaskManager.Repository;
using TaskManager.Service;

var builder = WebApplication.CreateBuilder(args);

string key = "0cbd2ce1-1f06-49f4-a693-0ae2f767db85";

// Add services to the container.

builder.Services.AddDbContext<TaskManagerContext> (
    options => options.UseNpgsql (
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();     

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

    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { SecuritySchema, Array.Empty<string>() }
    }
    );
});

builder.Services.AddControllers();

builder.Services.AddAuthentication(
    options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }

).AddJwtBearer(
    options => {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,  
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "devsallein",
            ValidAudience  = "application",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
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
