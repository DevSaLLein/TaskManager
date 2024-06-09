using System.Text.Json.Serialization;

namespace api.common
{
    public static class BuilderExtension
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            ApiConfiguration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public static void AddDataContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<TaskManagerContext> 
            (
                options => options.UseNpgsql(ApiConfiguration.ConnectionString) 
            );
        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ITaskRepository, TaskRepository>();

            builder.Services.AddTransient<ITaskService, TaskService>();  

            builder.Services.AddTransient<IUserRepository, UserRepository>();

            builder.Services.AddTransient<IViaCepIntegracao, ViaCepIntegracao>(); 

            builder.Services.AddTransient<ITokenService, TokenService>();

            builder.Services
                .AddRefitClient<IViaCepIntegracaoRefit>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://viacep.com.br/"))
            ; 

            builder.Services.AddControllers()
                .AddNewtonsoftJson(
                    options => 
                    {
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    }
                )
            ;

            builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>
            (
                options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
            );
        }  

        public static void AddDocumentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(
                option => 
                {
                    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Description = "API para o controle de Tarefas", Version = "v1" });

                    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter a valid token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "Bearer",
                        Reference = new OpenApiReference
                        {
                            Id = JwtBearerDefaults.AuthenticationScheme,
                            Type = ReferenceType.SecurityScheme
                        }
                    });

                    option.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type=ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });

                    option.CustomSchemaIds(x => x.FullName);
                }
            );
        }

        public static void AddIdentity(this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<UserIdentityApp, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 12;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
            }).AddEntityFrameworkStores<TaskManagerContext>();

            builder.Services.AddAuthentication(
                options => 
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultForbidScheme = 
                    options.DefaultScheme = 
                    options.DefaultSignInScheme =
                    options.DefaultSignOutScheme =  JwtBearerDefaults.AuthenticationScheme;
                }
            ).AddJwtBearer(
                options => 
                {
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
        }
    }
}