using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System.Linq.Expressions;

namespace Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
        protected readonly RepositoryContext _repositoriesContext;

        protected RepositoryBase(RepositoryContext repositoriesContext)
        {
            _repositoriesContext = repositoriesContext;
        }

        public void Create(T entity)
        {
            _repositoriesContext.Set<T>().Add(entity);
        }

        public IQueryable<T> FindAll(bool trackChanges)
        {
            return trackChanges
                ? _repositoriesContext.Set<T>()
                : _repositoriesContext.Set<T>().AsNoTracking();
        }

        public T? FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return trackChanges
                ? _repositoriesContext.Set<T>().Where(expression).SingleOrDefault() 
                : _repositoriesContext.Set<T>().Where(expression).AsNoTracking().SingleOrDefault();
        }

        public void Remove(T entity)
        {
            _repositoriesContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _repositoriesContext.Set<T>().Update(entity);
        }
    }
}
