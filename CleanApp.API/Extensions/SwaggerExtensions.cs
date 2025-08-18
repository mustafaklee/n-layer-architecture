using Microsoft.OpenApi.Writers;

namespace CleanApp.API.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerGenExt(this IServiceCollection services)
        {
            services.AddSwaggerGen();
            return services;
        }
        ,public static IApplicationBuilder UseSwaggerGenExt(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
