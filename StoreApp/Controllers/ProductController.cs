using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Repositories;
using Repositories.Contracts;
using Services.Contracts;
using Entities.RequestParameters;
using StoreApp.Models;

namespace StoreApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public ProductController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public IActionResult Index(ProductRequestParameters p)
        {
            var products = _serviceManager.ProductService.GetAllProductsWithDetails(p);
            var pagination = new Pagination()
            {
                ItemsPerPage = p.PageSize,
                CurrentPage = p.PageNumber,
                TotalItems = _serviceManager.ProductService.GetAllProducts(false).Count()
            };

            return View(new ProductListViewModel()
            {
                Products = products,
                Pagination = pagination
            });
        }

        public IActionResult Get([FromRoute(Name ="Id")] int Id)
        {
            // Product product = _repositoryManager.Product.First(p => p.ProductId.Equals(Id));
            var model = _serviceManager.ProductService.GetOneProduct(Id, false);
            ViewData["title"] = $" - {model?.ProductName}";
            return View(model);
        }
    }
}
