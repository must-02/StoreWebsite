namespace Entities.Models
{
    public class Cart
    {
        public List<CartLine> CartLines { get; set; }
        public Cart()
        {
            CartLines = new List<CartLine>();
        }

        public virtual void AddItem(Product product, int quantity)
        {
            CartLine? cartLine = CartLines.
                Where(cl => cl.CartLineProduct.ProductId.Equals(product.ProductId))
                .FirstOrDefault();

            if (cartLine is null)
            {
                CartLines.Add(new CartLine
                {
                    CartLineProduct = product,
                    Quantity = quantity
                });
            }
            else
            {
                cartLine.Quantity += quantity;
            }
        }

        public virtual void RemoveCartLine(Product product)
        {
            CartLines.RemoveAll(cl => cl.CartLineProduct.ProductId.Equals(product.ProductId));
        }
        public decimal ComputeTotalValue() => CartLines.Sum(cl => cl.CartLineProduct.ProductPrice * cl.Quantity);
        public virtual void Clear() => CartLines.Clear();
    }
}
