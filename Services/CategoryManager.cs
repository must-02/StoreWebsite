using Entities.Dtos;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class CategoryManager : ICategoryService
    {
        private readonly IRepositoryManager _repositoryManager;

        public CategoryManager(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public void CreateCategory(CategoryDtoForInsertion categoryDto)
        {
            Category category = new Category
            {
                CategoryName = categoryDto.CategoryName,
            };
            _repositoryManager.Category.CreateOneCategory(category);
            _repositoryManager.Save();
        }

        public IEnumerable<Category> GetAllCategories(bool trackChanges)
        {
            return _repositoryManager.Category.FindAll(trackChanges);
        }
    }
}
