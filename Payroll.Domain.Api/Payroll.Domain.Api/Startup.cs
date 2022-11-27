using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Payroll.Domain.Api.Configuration;
using Payroll.Domain.Api.Middlewares;
using Payroll.Domain.Shared.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Payroll.Domain.Api
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
            services.AddSwaggerGen();
            services.AddControllers();
            services.AddHttpContextAccessor();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.ConfigureSwagger();

            services.RegisterDependencies(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            
            app.UseRouting();
            NoCacheResponse(app);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<JwtMiddleware>();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payroll Domain Api");
                c.DisplayRequestDuration();
                c.DocExpansion(DocExpansion.List);
            });
        }

        /// <summary>
        ///     Set No Cache to api response
        /// </summary>
        /// <param name="app"></param>
        private static void NoCacheResponse(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                context.Response.GetTypedHeaders().CacheControl =
                    new CacheControlHeaderValue
                    {
                        MustRevalidate = true,
                        NoCache = true,
                        NoStore = true
                    };
                context.Response.Headers[HeaderNames.Pragma] =
                    new[] { "no-cache" };
                context.Response.Headers[HeaderNames.Expires] =
                    new[] { "0" };
                await next();
            });
        }
    }


}
