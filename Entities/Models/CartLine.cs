namespace Entities.Models
{
    public class CartLine
    {
        public int CartLineId { get; set; }
        public Product CartLineProduct { get; set; } = new();
        public int Quantity { get; set; }
    }
}
