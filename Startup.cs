using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebApiAttempt1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using WebApiAttempt1.Repositories;

namespace WebApiAttempt1
{
    public class Startup
    {
        public IConfiguration configuration { get; }
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = configuration.GetConnectionString("TestsBSUConnection");
            services.AddDbContext<TestsContext>(options => options.UseSqlServer(connection));
            services.AddScoped<ITestsRepository, TestsRepository>();
            services.AddControllers();
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "TestsBSU API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(x =>
           {
               x.SwaggerEndpoint("/swagger/v1/swagger.json", "TestsBSU API");
               x.RoutePrefix = "swagger";
           });

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
