using Member.Context;
using Member.Infrastructure.Abstraction.Interfaces;
using Member.Infrastructure.Abstraction.Repostory;
using Member.Infrastructure.Repostory;
using Member.BLL.Interfaces;
using Member.BLL.Manager;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Member.Service.Service.Interfaces;
using Member.Service.Service.Service;
using Member.Service.Model;
using System.Net;
using Microsoft.Extensions.Configuration;
using Member.Service.Profiles;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Member.Web.Api.Extentions
{
	public static class ServeiseCollectionExtention	
	{
		public static void AddRepostory(this IServiceCollection services)
		{
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
		}

		public static void AddDbContext(this IServiceCollection services, ConfigurationManager configurationManager)
		{
			var connectionString = configurationManager.GetConnectionString("WebApiDatabase");
			services.AddDbContext<MemberContext>(options => options.UseMySQL(configurationManager.GetConnectionString("WebApiDatabase")));
        }

		public static void AddEmailSender(this IServiceCollection services)
        {
			services.AddTransient<IEmailSender, EmailSender>();
		}

		public static void AddAccountManager(this IServiceCollection services)
		{
			services.AddScoped<IAccountManager, AccountManger>();
        }

        public static void AddAuthorizationService(this IServiceCollection services)
        {
			services.AddScoped<IAuthorizationService, JWTService>();
			
		}

		public static void AddAutoMapper(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(UserMapProfile));
            //services.AddControllersWithViews();
        }
        public static void ConfigureJWT(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
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
                        new string[]{}
                    }
                });
            });
        }
        public static void BindModel(this ConfigurationManager configuration, IServiceCollection services)
        {
            //         var jwtModel = new JWTModel();
            //var Issuer = configuration["Jwt:Issuer"];
            //var SecretKey = configuration["Jwt:SecretKey"];
            //var Audience = configuration["Jwt:Audience"];

            //         jwtModel.Issuer = Issuer;
            //jwtModel.SecretKey = SecretKey;
            //jwtModel.Audience = Audience;
            //services.Configure<JWTModel>(configuration.GetSection("Jwt"));

            services.AddAuthentication()
                   .AddJwtBearer(cfg =>
                   {
                       cfg.RequireHttpsMetadata = false;
                       cfg.SaveToken = true;

                       cfg.TokenValidationParameters = new TokenValidationParameters()
                       {
                           ValidIssuer = configuration.GetValue<string>("Jwt:Issuer"),
                           ValidAudience = configuration.GetValue<string>("Jwt:Audience"),
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:SecretKey"))),
                       };
                   });
        }
    }
}

