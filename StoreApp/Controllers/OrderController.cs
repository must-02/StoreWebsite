using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly Cart cart;

        public OrderController(IServiceManager serviceManager, Cart cart)
        {
            _serviceManager = serviceManager;
            this.cart = cart;
        }

        [Authorize]
        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout([FromForm] Order order)
        {
            if (cart.CartLines.Count() == 0)
                ModelState.AddModelError("", "Sorry, your cart is empty");

            if (ModelState.IsValid)
            {
                order.CartLines = cart.CartLines.ToArray();
                _serviceManager.OrderService.SaveOrder(order);
                cart.Clear();
                return RedirectToPage("/complete", new { OrderId = order.OrderId });
            }
            else
            {
                return View();
            }
        }
    }
}
