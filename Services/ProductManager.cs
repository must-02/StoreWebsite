using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;
using Repositories.Contracts;
using Services.Contracts;
using System.ComponentModel;

namespace Services
{
    public class ProductManager : IProductService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _autoMapper;

        public ProductManager(IRepositoryManager repositoryManager, IMapper autoMapper)
        {
            _repositoryManager = repositoryManager;
            _autoMapper = autoMapper;
        }

        public void CreateProduct(ProductDtoForInsertion productDto)
        {
            Product product = _autoMapper.Map<Product>(productDto);
            //Product product = new Product
            //{
            //    ProductName = productDto.ProductName,
            //    ProductPrice = productDto.ProductPrice,
            //    CategoryId = productDto.CategoryId
            //};
            _repositoryManager.Product.CreateOneProduct(product);
            _repositoryManager.Save();
        }

        public void DeleteOneProduct(int productId)
        {
            Product product = GetOneProduct(productId, false);
            if (product is not null)
            {
                _repositoryManager.Product.DeleteOneProduct(product);
                _repositoryManager.Save();
            }
        }

        public IEnumerable<Product> GetAllProducts(bool trackChanges)
        {
            return _repositoryManager.Product.GetAllProducts(trackChanges);
        }

        public IEnumerable<Product> GetAllProductsWithDetails(ProductRequestParameters p)
        {
            return _repositoryManager.Product.GetAllProductsWithDetails(p);
        }

        public IEnumerable<Product> GetLastestProducts(int n, bool trackChanges)
        {
            return _repositoryManager
                    .Product
                    .FindAll(trackChanges)
                    .OrderByDescending(p => p.ProductId)
                    .Take(n);
        }

        public Product? GetOneProduct(int id, bool trackChanges)
        {
            var product = _repositoryManager.Product.GetOneProduct(id, trackChanges);
            if (product is null)
                throw new Exception("Product not found!");
            return product;
        }

        public ProductDtoForUpdate? GetOneProductDtoForUpdate(int productId, bool trackChanges)
        {
            var product = GetOneProduct(id: productId, trackChanges);
            return _autoMapper.Map<ProductDtoForUpdate>(source: product);
        }

        public IEnumerable<Product> GetShowcaseProducts(bool trackChanges)
        {
            var showcaseProducts = _repositoryManager.Product.GetShowcaseProducts(trackChanges);
            return showcaseProducts;
        }

        public void UpdateOneProduct(Product product)
        {
            var entity = _repositoryManager.Product.GetOneProduct(product.ProductId, true);
            entity.ProductName = product.ProductName;
            entity.ProductPrice = product.ProductPrice;
            _repositoryManager.Save();
        }

        public void UpdateOneProductDtoForUpdate(ProductDtoForUpdate productDto)
        {
            //var entity = _repositoryManager.Product.GetOneProduct(productDto.ProductId, true);
            //entity.ProductName = productDto.ProductName;
            //entity.ProductPrice = productDto.ProductPrice;
            //entity.CategoryId = productDto.CategoryId;

            // Burada referans farklı olduğundan EFCore bunu izleyemez ve bu yüzden kaydedemez.
            var entity = _autoMapper.Map<Product>(productDto);
            _repositoryManager.Product.UpdateOneProduct(entity);
            _repositoryManager.Save();
        }
    }
}
