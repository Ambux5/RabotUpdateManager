using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using RabotUpdateManager.Abstractions;
using RabotUpdateManager.Managers;
using RabotUpdateManager.Options;
using System.Reflection;

namespace RabotUpdateManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DisplayOptions>(Configuration.GetSection(DisplayOptions.SectionName));
            services.Configure<EnviromentOptions>(Configuration.GetSection(EnviromentOptions.SectionName));

            services.AddSingleton<IDisplayManager, DisplayManager>();

            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "RabotUpdateManager",
                        Description =  "API for update managing rabot service and autoupdate new version",
                        Version = "v1",
                    });
                var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
                options.IncludeXmlComments(filePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(options => 
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "RabotUpdateManager API");
                options.RoutePrefix = "";
            });

        }
    }
}
