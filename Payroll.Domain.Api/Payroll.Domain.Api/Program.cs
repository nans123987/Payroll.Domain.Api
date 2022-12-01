using Microsoft.Net.Http.Headers;
using Payroll.Domain.Api.Middlewares;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Payroll.Domain.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json", optional: false)
                            .Build();

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.Configure<AppSettings>(config.GetSection("AppSettings"));
            builder.Services.ConfigureSwagger();

            builder.Services.RegisterDependencies(config);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payroll Domain Api");
                    c.DisplayRequestDuration();
                    c.DocExpansion(DocExpansion.List);
                });
            }

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();

            app.UseRouting();
            NoCacheResponse(app);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<JwtMiddleware>();

            app.MapControllers();

            app.Run();
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