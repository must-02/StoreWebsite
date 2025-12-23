using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(RepositoryContext repositoriesContext) : base(repositoriesContext)
        {
        }

        public IQueryable<Order> Orders => _repositoriesContext.Orders
            .Include(o => o.CartLines)
            .ThenInclude(cl => cl.CartLineProduct)
            .OrderBy(o => o.Shipped)
            .ThenByDescending(o => o.OrderId);

        public int NumberOfInProcess =>
            _repositoriesContext.Orders.Count(o => o.Shipped.Equals(false));

        public void Complete(int id)
        {
            var order = FindByCondition(o => o.OrderId.Equals(id), true);
            if (order is null)
                throw new Exception("Order is not found");
            order.Shipped = true;
        }

        public Order? GetOneOrder(int id) =>
            FindByCondition(o => o.OrderId.Equals(id), false);

        public void SaveOrder(Order order)
        {
            _repositoriesContext.AttachRange(order.CartLines.Select(cl => cl.CartLineProduct));
            if(order.OrderId == 0)
                _repositoriesContext.Orders.Add(order);
        }
    }
}
