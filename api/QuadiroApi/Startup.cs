using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using QuadiroApi.Entities;
using QuadiroApi.Repositories;
using QuadiroApi.Repositories.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace QuadiroApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public static IConfiguration Configuration { get; private set; }

		// This method gets called by the runtime. Use this method to add services to the container
		public void ConfigureServices(IServiceCollection services)
		{
			#region Swagger Configuration

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "Quadiro API Documentation",
					Version = "v1.0"
				});

				c.TagActionsBy(api =>
				{
					if (api.GroupName != null)
						return new[] { api.GroupName };

					if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
						return new[] { controllerActionDescriptor.ControllerName };

					throw new InvalidOperationException("Unable to determine tag for endpoint.");
				});
				c.DocInclusionPredicate((name, api) => true);

				c.MapType<ResponseModel>(() => new OpenApiSchema { Example = new OpenApiString(JsonConvert.SerializeObject(new ResponseModel(), Formatting.Indented)) });
				c.MapType<AuthenticationModel>(() => new OpenApiSchema { Example = new OpenApiString(JsonConvert.SerializeObject(new AuthenticationModel(), Formatting.Indented)) });
				c.MapType<AuthenticationResponseModel>(() => new OpenApiSchema { Example = new OpenApiString(JsonConvert.SerializeObject(new AuthenticationResponseModel(), Formatting.Indented)) });
				c.MapType<UserOtpModel>(() => new OpenApiSchema { Example = new OpenApiString(JsonConvert.SerializeObject(new UserOtpModel(), Formatting.Indented)) });
				c.MapType<ProductModel>(() => new OpenApiSchema { Example = new OpenApiString(JsonConvert.SerializeObject(new ProductModel(), Formatting.Indented)) });

				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});

			#endregion Swagger Configuration

			services.AddMvc().AddNewtonsoftJson();
			services.AddCors(options =>
			{
				options.AddDefaultPolicy(builder =>
					builder.SetIsOriginAllowed(_ => true)
						.AllowAnyMethod()
						.AllowAnyHeader()
						.AllowCredentials());
			});

			var dbConnectionString = Configuration.GetConnectionString("Database");
			services.AddTransient<IDbConnection>((sp) => new SqlConnection(dbConnectionString));

			services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
			services.AddTransient<IProductRepository, ProductRepository>();
			services.AddTransient<IGeneralSettingRepository, GeneralSettingRepository>();

			services.AddMvc();
			services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quadiro");
				c.RoutePrefix = string.Empty;
			});

			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();
			app.UseCors();
			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}