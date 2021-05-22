using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TestsBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using TestsBackend.Repositories;
using TestsBackend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TestsBackend.Authentication;
using Microsoft.IdentityModel.Tokens;
using TestsBackend.Interfaces;

namespace TestsBackend
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
            services.AddScoped<ITestsService, TestsService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ISubjectsService, SubjectsService>();
            services.AddControllers();
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "TestsBSU API", Version = "v1" });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // will be validation of the issuer executed?
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.ISSUER,
                        // will be validation of the audience executed?
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.AUDIENCE,
                        ValidateLifetime = true,
                        // setting security key
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
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

            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
