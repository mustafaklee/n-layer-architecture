using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using App.Repositories.Products;
using App.Application.Contracts.Persistence;
namespace App.Repositories.Extensions
{
    //	  <FrameworkReference Include="Microsoft.AspNetCore.App" />
    //project file'ye ekledik.apideki paketleri görebilmesi için.
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                var connectionStrings = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
                //connectionStrings!  !bu deger null olmayacak.
                options.UseMySql(
                    connectionStrings!.MySqlServer,
                    new MySqlServerVersion(new Version(8, 0, 0)),
                    mysqlServerOptionsAction =>
                    {
                        mysqlServerOptionsAction.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName);
                    });
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            //generic oldugu icin typeof ifadesi kullanıldı.
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //program.cs de dönen servicesi ilerde kullanabilmek icin
            return services;
        }
    }
}
