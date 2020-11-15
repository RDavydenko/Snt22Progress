	using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Snt22Progress.BussinesLogic.Interfaces;
using Snt22Progress.BussinesLogic.Services;
using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;
using Snt22Progress.DataAccess.Repositories;
using Snt22Progress.Logging;

namespace Snt22Progress.Web.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<AuthSettings>(f => new AuthSettings(Configuration));

			var authSettings = new AuthSettings(Configuration);
			// Авторизация по Bearer Token'у
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.RequireHttpsMetadata = authSettings.RequireHttpsMetadata;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = authSettings.ValidateIssuer,
						ValidIssuer = authSettings.Issuer,

						ValidateAudience = authSettings.ValidateAudience,
						ValidAudience = authSettings.Audience,

						ValidateLifetime = authSettings.ValidateLifetime,

						ValidateIssuerSigningKey = authSettings.ValidateIssuerSigningKey,
						IssuerSigningKey = authSettings.GetSymmetricSecurityKey()
					};
				});

			var dbConnection = Configuration.GetSection("ConnectionStrings")?.GetSection("DefaultPostgres")?.Value;

			// Репозитории
			services.AddTransient<IRepository<User, int>, UsersRepository>(f => new UsersRepository(dbConnection));
			services.AddTransient<IRepository<Role, int>, RolesRepository>(f => new RolesRepository(dbConnection));
			services.AddTransient<IRepository<Advertisement, int>, AdvertisementsRepository>(f => new AdvertisementsRepository(dbConnection));
			services.AddTransient<IRepository<AdvertisementFile, int>, AdvertisementFilesRepository>(f => new AdvertisementFilesRepository(dbConnection));
			services.AddTransient<IRepository<Choise, int>, ChoisesRepository>(f => new ChoisesRepository(dbConnection));
			services.AddTransient<IRepository<Document, int>, DocumentsRepository>(f => new DocumentsRepository(dbConnection));
			services.AddTransient<IRepository<Question, int>, QuestionsRepository>(f => new QuestionsRepository(dbConnection));
			services.AddTransient<IRepository<UserToChoise, int>, UserToChoisesRepository>(f => new UserToChoisesRepository(dbConnection));
			services.AddTransient<IRepository<UserToRole, int>, UserToRolesRepository>(f => new UserToRolesRepository(dbConnection));
			services.AddTransient<IRepository<ValuePair, int>, ValuePairsRepository>(f => new ValuePairsRepository(dbConnection));

			// Сервисы
			services.AddTransient<IAuthService, AuthService>();
			services.AddTransient<IPasswordHashService, PasswordHashService>();
			services.AddTransient<IProgressLogger, ProgressLogger>();

			services.AddControllers();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
