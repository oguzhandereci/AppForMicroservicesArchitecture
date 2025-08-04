namespace Order.Test.Domain
{
    using Order.Domain.Entities;
    using Order.Domain.Common;
    using Order.Domain.ValueObjects;
    using Order.Domain.Enums;
    using Order.Domain.Contants;

    public class OrderAggregateTest
    {
        [Fact]
        public void OrderBillingDetail_Create_Must_Return_Failure() 
        {
            // Arrange
            Result<OrderBillingDetail> orderBillingDetailResult = OrderBillingDetail.Create(
                firstName: "",
                lastName: "Doe",
                addressLine: "123 Main St",
                state: "CA",
                zipCode: "12345",
                country: "USA",
                emailAddress: "abc@abc.com"
            );
            // Act
            // Assert
            Assert.False(orderBillingDetailResult.IsSuccess);
            Assert.Equal(Order.Domain.Contants.ErrorMessage.FirstNameIsRequired, orderBillingDetailResult?.Error?.ErrDescription);
        }

        [Fact]
        public void OrderBillingDetail_Create_Must_Return_Success()
        {
            // Arrange
            Result<OrderBillingDetail> orderBillingDetailResult = OrderBillingDetail.Create(
                firstName: "John",
                lastName: "Doe",
                addressLine: "123 Main St",
                state: "CA",
                zipCode: "12345",
                country: "USA",
                emailAddress: "abc@abc.com"
            );
            // Act
            // Assert
            Assert.True(orderBillingDetailResult.IsSuccess);
            Assert.Null(orderBillingDetailResult?.Error);
        }

        [Fact]
        public void Order_Create_Must_Return_Failure()
        {
            // Arrange
            Result<Order> createOrderResult = Order.CreateOrder(
                userId: "",
                totalPrice: 100.0m,
                orderStatus: OrderStatus.Submitted,
                firstName: "John",
                lastName: "Doe",
                addressLine: "123 Main St",
                country: "USA",
                state: "CA",
                zipCode: "12345",
                emailAddress:"abc@abc.com"
                );
            // Act
            // Assert
            Assert.False(createOrderResult.IsSuccess);
            Assert.Equal(ErrorMessage.UserIdRequired, createOrderResult?.Error?.ErrDescription);
        }

        [Fact]
        public void Order_Create_Must_Return_Success()
        {
            // Arrange
            Result<Order> createOrderResult = Order.CreateOrder(
                userId: "jdoe",
                totalPrice: 100.0m,
                orderStatus: OrderStatus.Submitted,
                firstName: "John",
                lastName: "Doe",
                addressLine: "123 Main St",
                country: "USA",
                state: "CA",
                zipCode: "12345",
                emailAddress: "abc@abc.com"
                );
            // Act
            // Assert
            Assert.True(createOrderResult.IsSuccess);
            Assert.Null(createOrderResult?.Error);
        }

    }
}
