using App.Services.ExceptionHandler;
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


            //sira onemlidir, cunku criticalexception'da hatayı ele almıyoruz.sadece hata geldiğinde 
            //gerekli işlemleri yapıp geri dönüyoruz false ile.
            //her seferinde criticalexception'a girer, daha sonra critical'in return tipi false oldugu ıcın globalexception'a gider. 
            services.AddExceptionHandler<CriticalExceptionHandler>();
            //burda ise hatayı işleme evresine denk geliyoruz.bu bölüm true döndüğü için exception başka bir yere takılmaz.
            services.AddExceptionHandler<GlobalExceptionHandler>();

            return services;
        }
    }
}
