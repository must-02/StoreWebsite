//using System.ComponentModel.DataAnnotations; artık kullanmıyoruz SOLID den dolayı bu class hem validation hem de entity olamaz

namespace Entities.Models;

public class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public decimal ProductPrice { get; set; }
    public string? ProductSummary { get; set; }
    public string? ProductImageUrl { get; set; }

    public int? CategoryId { get; set; } // Foreign Key
    public Category? ProductCategory { get; set; } // Navigation property

    public bool ShowCase { get; set; }
}
