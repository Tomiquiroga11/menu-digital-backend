using System.Text;
using MenuDigital.Api.Data;
using MenuDigital.Api.Entities;
using MenuDigital.Api.Repositories.Implementations;
using MenuDigital.Api.Repositories.Interfaces;
using MenuDigital.Api.Services;
using MenuDigital.Api.Services.Implementations;
using MenuDigital.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace MenuDigital.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularApp",
                policy =>
                {
                    policy.WithOrigins("http://localhost:4200") 
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        
        builder.Services.AddAuthorization(); 

        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Introduce 'Bearer' [espacio] y luego tu token en el campo de texto.\n\nEjemplo: \"Bearer eyJhbGciOiJ...\""
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

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
        
        builder.Services.AddScoped<IRestauranteRepository, RestauranteRepository>();
        builder.Services.AddScoped<IMenuRepository, MenuRepository>();
        builder.Services.AddScoped<IRestauranteService, RestauranteService>();
        builder.Services.AddScoped<IMenuService, MenuService>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddSingleton<IPasswordHasher<Restaurante>, PasswordHasher<Restaurante>>();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<IPedidoService, PedidoService>();
        
        var authSettings = builder.Configuration.GetSection("Authentication");
        var secretKey = authSettings["SecretForKey"];
        var issuer = authSettings["Issuer"];
        var audience = authSettings["Audience"];

        if (string.IsNullOrEmpty(secretKey))
            throw new ArgumentNullException(nameof(secretKey), "Authentication:SecretForKey no esta configurada");
        if (string.IsNullOrEmpty(issuer))
            throw new ArgumentNullException(nameof(issuer), "Authentication:Issuer no esta configurado");
        if (string.IsNullOrEmpty(audience))
            throw new ArgumentNullException(nameof(audience), "Authentication:Audience no esta configurado");

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            
                    ValidateIssuer = true, 
                    ValidIssuer = issuer,
            
                    ValidateAudience = true, 
                    ValidAudience = audience
                };
            });
        
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("AllowAngularApp");
        app.UseAuthentication(); 
        app.UseAuthorization();  

        app.MapControllers();
        
        app.Run();
    }
}