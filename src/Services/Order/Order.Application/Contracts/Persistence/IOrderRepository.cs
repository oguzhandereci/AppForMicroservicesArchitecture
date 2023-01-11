namespace Order.Application.Contracts.Persistence
{
    using Order.Domain.Entities;
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
    }
}
