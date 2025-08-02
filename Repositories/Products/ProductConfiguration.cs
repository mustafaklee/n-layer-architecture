using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Repositories.Products
{
    //dbcontext'i sade bırakmak için configurasyon ayarları burda yapılacak.
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Price).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Stock).IsRequired();
        }
    }
}
