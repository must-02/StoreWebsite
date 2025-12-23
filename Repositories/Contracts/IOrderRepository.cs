using Entities.Models;

namespace Repositories.Contracts
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        public Order? GetOneOrder(int id);
        public int NumberOfInProcess { get; }
        void Complete(int id);
        void SaveOrder(Order order);
    }
}
