using Entities.Dtos;
using Entities.RequestParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Contracts;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public ProductController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public IActionResult Index(ProductRequestParameters p)
        {
            var model = _serviceManager.ProductService.GetAllProductsWithDetails(p);
            ViewData["title"] = " - Products";
            return View(model);
        }

        public IActionResult Create()
        {
            //ViewBag.Categories = _serviceManager.CategoryService.GetAllCategories(false);
            ViewBag.Categories = GetCategoriesSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ProductDtoForInsertion productDto, IFormFile formFile)
        {
            if (ModelState.IsValid)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", formFile.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
                productDto.ProductImageUrl = String.Concat("/images/", formFile.FileName);
                _serviceManager.ProductService.CreateProduct(productDto);
                TempData["success"] = $"\"{productDto.ProductName}\" has been added";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Update([FromRoute(Name = "id")] int ProductId)
        {
            ViewBag.Categories = GetCategoriesSelectList();
            var model = _serviceManager.ProductService.GetOneProductDtoForUpdate(ProductId, false);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] ProductDtoForUpdate productDto, IFormFile formFile)
        {
            if (ModelState.IsValid)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", formFile.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
                productDto.ProductImageUrl = String.Concat("/images/", formFile.FileName);
                _serviceManager.ProductService.UpdateOneProductDtoForUpdate(productDto);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete([FromRoute(Name ="id")] int ProductId)
        {
            TempData["danger"] = $"{_serviceManager.ProductService.GetOneProduct(ProductId, false).ProductName} has been deleted";
            _serviceManager.ProductService.DeleteOneProduct(ProductId);
            return RedirectToAction("Index");
        }

        private SelectList GetCategoriesSelectList()
        {
            return new SelectList(
                items: _serviceManager.CategoryService.GetAllCategories(false),
                dataValueField: "CategoryId",
                dataTextField: "CategoryName",
                selectedValue: "1"
                );
            ;
        }
    }
}
