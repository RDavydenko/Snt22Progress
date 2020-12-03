using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
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
using Microsoft.OpenApi.Models;
using Snt22Progress.BussinesLogic;
using Snt22Progress.BussinesLogic.Interfaces;
using Snt22Progress.BussinesLogic.Services;
using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;
using Snt22Progress.DataAccess.Repositories;
using Snt22Progress.DataAccess.Repositories.Interfaces;
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
			// Настройки авторизации по токену
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

			// Сервис для безболезненного проброса всяких констант (к примеру из appsettings)
			services.AddSingleton<ConfigurationService>(f =>
			{
				return new ConfigurationService(new BussinesLogic.Models.UploadedFilesSettings(
						documentsFilesFolderRelativePath: Configuration.GetSection("UploadedFilesSettings")?.GetSection("DocumentsFilesFolderRelativePath")?.Value,
						advertisementFilesFolderRelativePath: Configuration.GetSection("UploadedFilesSettings")?.GetSection("AdvertisementFilesFolderRelativePath")?.Value,
						debtorFilesFolderRelativePath: Configuration.GetSection("UploadedFilesSettings")?.GetSection("DebtorFilesFolderRelativePath")?.Value
					));
			});

			// Репозитории (таблицы)
			services.AddTransient<IRepository<User, int>, UsersRepository>(f => new UsersRepository(dbConnection));
			services.AddTransient<IRepository<Role, int>, RolesRepository>(f => new RolesRepository(dbConnection));
			services.AddTransient<IRepository<Advertisement, int>, AdvertisementsRepository>(f => new AdvertisementsRepository(dbConnection));
			services.AddTransient<IRepository<AdvertisementFile, int>, AdvertisementFilesRepository>(f => new AdvertisementFilesRepository(dbConnection));
			services.AddTransient<IRepository<Choise, int>, ChoisesRepository>(f => new ChoisesRepository(dbConnection));
			services.AddTransient<IRepository<Post, int>, PostsRepository>(f => new PostsRepository(dbConnection));
			services.AddTransient<IRepository<Document, int>, DocumentsRepository>(f => new DocumentsRepository(dbConnection));
			services.AddTransient<IRepository<Question, int>, QuestionsRepository>(f => new QuestionsRepository(dbConnection));
			services.AddTransient<IRepository<Legislation, int>, LegislationsRepository>(f => new LegislationsRepository(dbConnection));
			services.AddTransient<IRepository<DebtorFile, int>, DebtorFilesRepository>(f => new DebtorFilesRepository(dbConnection));
			services.AddTransient<IRepository<UserToChoise, int>, UserToChoisesRepository>(f => new UserToChoisesRepository(dbConnection));
			services.AddTransient<IRepository<UserToRole, int>, UserToRolesRepository>(f => new UserToRolesRepository(dbConnection));
			services.AddTransient<IRepository<ValuePair, int>, ValuePairsRepository>(f => new ValuePairsRepository(dbConnection));

			// Репозитории (представления)
			services.AddTransient<IViewRepository<PostView, int>, PostViewsRepository>(f => new PostViewsRepository(dbConnection));
			services.AddTransient<IViewRepository<DocumentView, int>, DocumentViewsRepository>(f => new DocumentViewsRepository(dbConnection));
			services.AddTransient<IViewRepository<AdvertisementView, int>, AdvertisementViewsRepository>(f => new AdvertisementViewsRepository(dbConnection));
			services.AddTransient<IViewRepository<QuestionView, int>, QuestionViewsRepository>(f => new QuestionViewsRepository(dbConnection));

			// Расширенные репозитории с доп.возможностями
			services.AddTransient<IPostViewRepository, PostViewsRepository>(f => new PostViewsRepository(dbConnection));

			// Сервисы
			services.AddTransient<IAuthService, AuthService>();
			services.AddTransient<IPasswordHashService, PasswordHashService>();
			services.AddTransient<IProgressLogger, ProgressLogger>();
			services.AddTransient<IPostsService, PostsService>();
			services.AddTransient<IUsersService, UsersService>();
			services.AddTransient<IDocumentsService, DocumentsService>();
			services.AddTransient<IGovernmentService, GovernmentService>();
			services.AddTransient<ILegislationService, LegislationService>();
			services.AddTransient<IDebtorFilesService, DebtorFilesService>();
			services.AddTransient<IAdvertisementsService, AdvertisementsService>();
			services.AddTransient<IQuestionsService, QuestionsService>();

			// Маппер
			services.AddTransient<IMapper>(f => (new MapperConfiguration(cfg => cfg.AddMaps(new Assembly[] { BussinesLogicAssembly.Assembly })))
				.CreateMapper());

			services.AddControllers();

			// Сваггер
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Snt 22 Progress API", Version = "v1" });
			});

			// CORS - для работы с фронтендом
			services.AddCors();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseSwagger();
			app.UseSwaggerUI(opt =>
			{
				opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Snt 22 Progress API");
			});

			// Разрешить использование статических файлов (для загруженных фото и т.д.)
			var requestPathForStaticFiles = "/Upload";
			app.UseStaticFiles(requestPathForStaticFiles);

			// Класс для хранения и удобной передачи зависимостей, чтобы не городить кучу параметров в конструкторах
			Global.Initialize(
				authService: app.ApplicationServices.GetService<IAuthService>()
			);

			// Настройка CORS
			var corsSettings = Configuration.GetSection("CorsSettings");
			var allowedMethods = corsSettings?.GetSection("AllowedMethods")?.Value?.Split(',') ?? Array.Empty<string>();
			var allowedHeaders = corsSettings?.GetSection("AllowedHeaders")?.Value?.Split(',') ?? Array.Empty<string>();
			var allowedOrigins = corsSettings?.GetSection("AllowedOrigins")?.Value?.Split(',') ?? Array.Empty<string>();
			app.UseCors(builder =>
			{
				builder.WithMethods(allowedMethods);
				builder.WithHeaders(allowedHeaders);
				builder.WithOrigins(allowedOrigins);
			});

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
