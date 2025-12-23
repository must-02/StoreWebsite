using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public record ProductDto
    {
        public int ProductId { get; init; }

        [Required(ErrorMessage = "Product Name is required")]
        public string? ProductName { get; init; }

        [Required(ErrorMessage = "Product Price is required")]
        public decimal ProductPrice { get; init; }

        [StringLength(240)]
        public string? ProductSummary { get; init; }
        public string? ProductImageUrl { get; set; }
        public int? CategoryId { get; init; } // Foreign Key
    }
}
