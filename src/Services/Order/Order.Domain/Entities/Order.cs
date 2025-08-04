using Order.Domain.Common;
using Order.Domain.Contants;
using Order.Domain.Enums;
using Order.Domain.ValueObjects;

namespace Order.Domain.Entities
{
    public class Order : EntityBase
    {
        private const string StackTrace = "Order.Domain.Entities.Order";
        public Guid UserId { get; }
        public decimal TotalPrice { get;}
        public OrderStatus OrderStatus { get;}
        public OrderBillingDetail OrderBillingDetail { get;}

        private Order(string userId, decimal totalPrice, OrderStatus orderStatus, OrderBillingDetail orderBillingDetail)
        {
            UserId = Guid.Parse(userId);
            TotalPrice = totalPrice;
            OrderStatus = orderStatus;
            OrderBillingDetail = orderBillingDetail;
        }

        public static Result<Order> CreateOrder(string userId, decimal totalPrice, OrderStatus orderStatus, string firstName, string lastName, string emailAddress, string addressLine, string country, string state, string zipCode) 
        {

            Result<OrderBillingDetail> billingDetailResult = OrderBillingDetail.Create(firstName, lastName, emailAddress, addressLine, country, state, zipCode);

            if (billingDetailResult.IsFailure && billingDetailResult.Error is not null)
            {
                return Result<Order>.Failure(billingDetailResult.Error);
            }

            if (string.IsNullOrWhiteSpace(userId))
            {
                return Result<Order>.Failure(new Error(ErrorMessage.UserIdRequired, StackTrace + " - CreateOrder"));
            }

            if (totalPrice <= 0)
            {
                return Result<Order>.Failure(new Error(ErrorMessage.TotalPriceMustBeGreaterThanZero, StackTrace + " - CreateOrder"));
            }

            if (!Enum.IsDefined(typeof(OrderStatus), orderStatus))
            {
                return Result<Order>.Failure(new Error(ErrorMessage.InvalidOrderStatus, StackTrace + " - CreateOrder"));
            }

            if (billingDetailResult.Value is null)
            {
                return Result<Order>.Failure(new Error(ErrorMessage.BillingDetailIsRequired, StackTrace + " - CreateOrder"));
            }

            return Result<Order>.Success(new(userId, totalPrice, orderStatus, billingDetailResult.Value));
        }
    }
}
