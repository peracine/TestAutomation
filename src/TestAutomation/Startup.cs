using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestAutomation.Data;
using TestAutomation.Interfaces;
using TestAutomation.Services;

namespace TestAutomation
{
    public class Startup
    {
        readonly string _globalCorsPolicy = "CorsPolicy";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TestAutomationContext>(opt => opt.UseInMemoryDatabase("TestAutomation"));
            services.AddScoped<ITextRepository, TextRepository>();
            services.AddScoped<ITextServices, TextsServices>();
            services.AddScoped<ILog, Log>();
            
            services.AddCors(options =>
            {
                options.AddPolicy(_globalCorsPolicy,
                builder => { builder.WithOrigins("*");});
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(_globalCorsPolicy);
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
