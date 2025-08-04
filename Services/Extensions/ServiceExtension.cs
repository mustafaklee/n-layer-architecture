using App.Services.Products;
using App.Services.Products.Mapping;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace App.Services.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddFluentValidationAutoValidation();
            //bu dizinde ara dosyaları.
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            //automapper DI container
            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            return services;
        }
    }
}
