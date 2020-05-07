using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebApiAttempt1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Routing;
using System;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.AspNet.OData.Builder;
using WebApiAttempt1.ViewModels;

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
            services.AddControllers();
            //services.AddOData();
            //services.AddControllers(mvcOptions =>
            //    mvcOptions.EnableEndpointRouting = false);
            //services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("index.html");

            app.UseStaticFiles();
            app.UseDefaultFiles(options);
            app.UseRouting();

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

            //app.UseMvc(routeBuilder =>
            //{
            //    routeBuilder.Select().Filter().OrderBy();
            //    routeBuilder.MapODataServiceRoute("tests", "api", GetEdmModel());
            //});
        }

        //private IEdmModel GetEdmModel()
        //{
        //    var odataBuilder = new ODataConventionModelBuilder();
        //    odataBuilder.EntitySet<TestWithObjectSubject>("Tests");

        //    return odataBuilder.GetEdmModel();
        //}
    }
}
