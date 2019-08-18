using System;
using System.Collections.Generic;
using System.Linq;
using Prophet.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Prophet.Entities;
using Prophet.Operations;

namespace Prophet
{
    public class Startup
    {
        IConfiguration _config;

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpClient();

            services.AddEntityFrameworkNpgsql()
                  .AddDbContext<ProphetContext>(
                      options => options.UseNpgsql(_config.GetConnectionString("prophet"))
                  );

            services.AddTransient<VK.OpenApi.VkOpenApiClient>();
            services.AddSingleton<IJsonSerializer, SnakeCaseJsonSerializer>();
            services.AddTransient<ReadModel>();

            services.AddTransient<Mirgation>();
            services.AddTransient<PullArticleOperation>();

            services.AddTransient<ReadNewsEntityOperation>();
            services.AddTransient<CreateUserEntityOperation>();
            services.AddTransient<SaveArticleEntityOperation>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            Mirgation migration
        )
        {
            migration.Migrate();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
