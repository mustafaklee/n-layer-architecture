using CleanApp.API.ExceptionHandler;

namespace CleanApp.API.Extensions
{
    public static class ExceptionHandlerExtensions
    {
        public static IServiceCollection ExceptionHandlerExt(this IServiceCollection services)
        {
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
