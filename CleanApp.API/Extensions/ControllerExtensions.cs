using CleanApp.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CleanApp.API.Extensions
{
    public static class ControllerExtensions
    {
        public static IServiceCollection AddControllersExt(this IServiceCollection services)
        {
            services.AddControllers();


            //fluent validation kullanacağımız için default microsoft tarafından gelen hata mesajlarını kapatıyoruz.
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

            services.AddControllers(options =>
            {
                options.Filters.Add<FluentValidationFilter>();
                //referans tipliler için(örneğin string) nullable kontrolü yapma.custom validation için gerekli.
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            });

            return services;
        }
    }
}
