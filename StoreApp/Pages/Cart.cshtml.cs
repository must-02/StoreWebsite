using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Contracts;
using StoreApp.Infrastructure.Extensions;

namespace StoreApp.Pages
{
    public class CartModel : PageModel
    {
        private readonly IServiceManager _serviceManager;
        public Cart _cart { get; set; }
        public string ReturnUrl { get; set; } = "/";

        public CartModel(IServiceManager serviceManager, Cart cartService)
        {
            _serviceManager = serviceManager;
            _cart = cartService;
        }

        public void OnGet(int productId, string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            //_cart = HttpContext.Session.GetJson<Cart>("cart");
        }

        public IActionResult OnPost(int productId, string _returnUrl)
        {
            Product? product = _serviceManager.ProductService.GetOneProduct(productId, false);

            if (product is not null)
            {
                //_cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                _cart.AddItem(product, 1);
                //HttpContext.Session.SetJson<Cart>("cart", _cart);
            }

            return RedirectToPage(new { returnUrl = _returnUrl });
        }

        public IActionResult OnPostRemove(int productId, string returnUrl)
        {
            //_cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            _cart.RemoveCartLine(_cart.CartLines.First(cl => cl.CartLineProduct.ProductId.Equals(productId)).CartLineProduct);
            //HttpContext.Session.SetJson<Cart>("cart", _cart);
            return Page();
        }
    }
}
