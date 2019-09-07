using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Prophet.Dialog.Operations;

namespace Prophet.Dialog
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpClient();

            services.AddSingleton<DequeueOperation>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
