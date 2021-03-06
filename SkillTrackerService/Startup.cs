using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SkillTrackerService.DbContext;
using SkillTrackerService.Models;
using SkillTrackerService.Services;

namespace StockMarketService
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
            services.Configure<EngineerProfileDatabaseSettings>(
                Configuration.GetSection(nameof(EngineerProfileDatabaseSettings)));

            services.AddSingleton<IEngineerProfileDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<EngineerProfileDatabaseSettings>>().Value);
            services.AddSingleton<IMongoProfileDBContext, MongoProfileDBContext>();
            services.AddSingleton<IProfileService, ProfileService>();

            services.AddControllers();

            services.AddMemoryCache();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Skill Tracker Service API",
                    Version = "v2",
                    Description = "Skill Tracker Service",
                });
            });

            services.AddHealthChecks();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
            });
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v2/swagger.json", "PlaceInfo Services"));
        }
    }
}
