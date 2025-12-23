using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductId);
            builder.Property(p => p.ProductName).IsRequired();
            builder.Property(p => p.ProductPrice).IsRequired();

            builder.HasData(
                    new Product() { ProductId = 1, CategoryId = 2, ProductImageUrl = "/images/1.jpg", ProductName = "Computer", ProductPrice = 17_000, ShowCase = false },
                    new Product() { ProductId = 2, CategoryId = 2, ProductImageUrl = "/images/2.jpg", ProductName = "Keyboard", ProductPrice = 1_000, ShowCase = false },
                    new Product() { ProductId = 3, CategoryId = 2, ProductImageUrl = "/images/3.jpg", ProductName = "Mouse", ProductPrice = 500, ShowCase = false },
                    new Product() { ProductId = 4, CategoryId = 2, ProductImageUrl = "/images/4.jpg", ProductName = "Monitor", ProductPrice = 7_000, ShowCase = false },
                    new Product() { ProductId = 5, CategoryId = 2, ProductImageUrl = "/images/5.jpg", ProductName = "Deck", ProductPrice = 1_500, ShowCase = false },
                    new Product() { ProductId = 6, CategoryId = 1, ProductImageUrl = "/images/6.jpg", ProductName = "Beyaz Gemi", ProductPrice = 150, ShowCase = false },
                    new Product() { ProductId = 7, CategoryId = 1, ProductImageUrl = "/images/7.jpg", ProductName = "Uzun Hikaye", ProductPrice = 85, ShowCase = false },
                    new Product() { ProductId = 8, CategoryId = 1, ProductImageUrl = "/images/8.jpg", ProductName = "PIC16F877A", ProductPrice = 185, ShowCase = true },
                    new Product() { ProductId = 9, CategoryId = 2, ProductImageUrl = "/images/9.jpg", ProductName = "Britannica", ProductPrice = 850, ShowCase = true },
                    new Product() { ProductId = 10, CategoryId = 1, ProductImageUrl = "/images/10.jpg", ProductName = "Kulaklık", ProductPrice = 805, ShowCase = true }
                );
        }
    }
}
