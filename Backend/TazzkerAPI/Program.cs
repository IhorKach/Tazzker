using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Tazzker.Application.Interfaces;
using Tazzker.Application.Services;
using Tazzker.Infrastructure.Contexts;
using Tazzker.Infrastructure.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using TazzkerAPI.Middleware;
using Microsoft.AspNetCore.Mvc;
namespace TazzkerAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			var connectionString = builder.Configuration.GetConnectionString("Default");

			builder.Services.AddDbContext<TazzkerDbContext>(options => options.UseNpgsql(connectionString));

			builder.Services.AddScoped<IAuthService, AuthService>();
			builder.Services.AddScoped<IUserRepository, UserRepository>();


			builder.Services.AddScoped<IListRepository, ListRepository>();
			builder.Services.AddScoped<IListService, ListService>();

			builder.Services.AddScoped<ISublistRepository, SublistRepository>();
			builder.Services.AddScoped<ISublistService, SublistService>();

			builder.Services.AddScoped<ITaskRepository, TaskRepository>();
			builder.Services.AddScoped<ITaskService, TaskService>();

			builder.Services.AddScoped<INoteRepository, NoteRepository>();
			builder.Services.AddScoped<INoteService, NoteService>();


			builder.Services.AddHttpContextAccessor();
			builder.Services.AddScoped<IUserContext, UserContext>();

			builder.Services.AddControllers();

			builder.Services.AddEndpointsApiExplorer();

			builder.Services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo { Title = "Tazzker API", Version = "v1" });

				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "Введите токен в формате Bearer:{token}",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
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
							},
							Scheme = "oauth2",
							Name = "Bearer",
							In = ParameterLocation.Header
						},
						new List<string>()
					}
				});
			});


			//auth through cookie
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(options =>
				{
					options.Events = new JwtBearerEvents
					{
						OnMessageReceived = context =>
						{
							// Read JWT token from cookie
							if (context.Request.Cookies.ContainsKey("access_token"))
							{
								context.Token = context.Request.Cookies["access_token"];
							}
							return Task.CompletedTask;
						}
					};



					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(
							Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
						),
						ValidateLifetime = true,
						ClockSkew = TimeSpan.Zero
					};
				});


			builder.WebHost.ConfigureKestrel(options =>
			{
				options.ListenAnyIP(5001, listenOptions =>
				{
					listenOptions.UseHttps(); // HTTPS
				});
			});


			var frontendUrls = builder.Configuration.GetSection("ServerConfig:FrontendUrls").Get<string[]>();

			builder.Services.AddCors(options =>
			{

				options.AddPolicy("AllowFrontend", policy =>
				{
					policy
						.WithOrigins(frontendUrls!)
						.AllowAnyHeader()
						.AllowAnyMethod()
						.AllowCredentials();
				});
			});


			//errors convertor to readeable for user way
			builder.Services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = context =>
				{
					var errors = context.ModelState
						.Where(e => e.Value?.Errors.Count > 0)
						.SelectMany(x => x.Value!.Errors)
						.Select(x => x.ErrorMessage)
						.ToList();

					var joined = string.Join(" | ", errors);

					return new BadRequestObjectResult(new { message = joined });
				};
			});



			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseRouting();
			app.UseCors("AllowFrontend");

			app.UseHttpsRedirection();
			app.UseMiddleware<ExceptionHandlingMiddleware>();

			app.UseAuthentication();
			app.UseAuthorization();



			app.MapControllers();

			app.Run();
		}
	}
}
