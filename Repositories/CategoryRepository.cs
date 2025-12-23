using Entities.Models;
using Repositories.Contracts;

namespace Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext repositoriesContext) : base(repositoriesContext)
        {
            
        }

        public void CreateOneCategory(Category category) => Create(category);
    }
}
