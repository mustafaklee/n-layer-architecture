using App.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace App.Repositories
{                       //burdaki options'a appsettings.json'daki connection string gelecek
    public class AppDbContext(DbContextOptions<AppDbContext> options):DbContext(options)
    {
        public DbSet<Product> Products { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //20 tane configuration dosyamız oldugunu düşünürsek cok uzun bir işlem olur
            //modelBuilder.ApplyConfiguration(new ProductConfiguration);
            //bunun yerine biz assembly olarak IEntityTypeConfiguration interfacesini implemente etmiş olan classları alıyoruz.

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            base.OnModelCreating(modelBuilder);
        }
    }
}
