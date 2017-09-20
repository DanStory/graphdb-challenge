using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;
using Swashbuckle.AspNetCore.Swagger;

namespace GraphDb.API
{
	public partial class Startup
    {
	    private readonly Container _container = new Container()
	    {
		    Options = {DefaultScopedLifestyle = new AsyncScopedLifestyle()}
	    };

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
		{
			this.ConfigureContainerService(services);
			this.ConfigureMvcService(services);
			this.ConfigureSwaggerService(services);
		}

        private void ConfigureMvcService(IServiceCollection services)
        {
	        services.AddCors(options =>
	        {
				options.AddPolicy("CORS", builder =>
				{
					builder.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader()
						.AllowCredentials();
				});
	        });

			services.AddMvc();

	        services.AddSignalR(config =>
	        {
		        config.JsonSerializerSettings.Formatting = Formatting.None;
				config.JsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
	        });
		}

        private void ConfigureContainerService(IServiceCollection services)
        {
	        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
	        services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(this._container));

	        services.AddSingleton<IHubActivator<EventHub>>(new SimpleInjectorHubActivator<IEventHub, EventHub>(this._container));

			services.EnableSimpleInjectorCrossWiring(this._container);
			services.UseSimpleInjectorAspNetRequestScoping(this._container);
        }


		private void ConfigureSwaggerService(IServiceCollection services)
	    {
		    services.AddSwaggerGen(options =>
		    {
			    options.SwaggerDoc("v1", new Info() { Title = "GraphDb API", Version = "v1" });
		    });
	    }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

			this.ConfigureContainer(app, env);

	        this.ConfigureMvc(app, env);

			this.ConfigureSwagger(app, env);

	        this._container.Verify(env.IsDevelopment() ? VerificationOption.VerifyAndDiagnose : VerificationOption.VerifyOnly);
		}

	    private void ConfigureMvc(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseCors("CORS");
			app.UseMvc();

			app.UseSignalR(config =>
			{
				config.MapHub<EventHub>("events");
			});
		}

		private void ConfigureContainer(IApplicationBuilder app, IHostingEnvironment env)
	    {
		    this._container.RegisterMvcControllers(app);

			Registry.Register(this._container);
		}

	    private void ConfigureSwagger(IApplicationBuilder app, IHostingEnvironment env)
	    {
		    app.UseSwagger(options =>
		    {
			    options.RouteTemplate = "{documentName}/swagger.json";
		    });

		    app.UseSwaggerUI(options =>
		    {
			    options.SwaggerEndpoint("/v1/swagger.json", "GraphDb API v1");
		    });
	    }

	}
}
