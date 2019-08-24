
using Prophet.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prophet.Operations;
using Prophet.ReadModel;
using Prophet.EntityOperations;

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

            services.AddDbContext<ProphetContext>(
                options => options.UseSqlite(_config.GetConnectionString("prophet"))
            );

            services.AddTransient<VK.OpenApi.VkOpenApiClient>();
            services.AddSingleton<IJsonSerializer, SnakeCaseJsonSerializer>();

            services.AddTransient<Mirgation>();
            //
            // services.AddTransient<AddFeedOperation>();

            /* Entity Operations */
            services.AddTransient<ReadNewsEntityOperation>();
            services.AddTransient<CreateUserEntityOperation>();
            services.AddTransient<SaveArticleEntityOperation>();

            /* Operations */
            services.AddTransient<StartOperation>();

            /* Read Models */
            services.AddTransient<UserReadModel>();
            services.AddTransient<ArticleReadModel>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            Mirgation migration
        )
        {
            migration.Migrate();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
