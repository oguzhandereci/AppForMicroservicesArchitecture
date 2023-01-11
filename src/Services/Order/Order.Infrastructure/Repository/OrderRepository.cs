

namespace Ordering.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Order.Application.Contracts.Persistence;
    using Order.Infrastructure.Persistence;
    using Order.Domain.Entities;
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            var orderList = await _dbContext.Orders
                                    .Where(o => o.UserName == userName)
                                    .ToListAsync();
            return orderList;
        }
    }
}