using Entities.Models;

namespace Services.Contracts
{
    public interface IOrderService
    {
        public IQueryable<Order> Orders { get; }
        int NumberOfInProcess { get; }
        Order? GetOneOrder(int id);
        void Complete(int id);
        void SaveOrder(Order order);
    }
}
