using Discount.Grpc.Protos;
using System;
using System.Threading.Tasks;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService : IDiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountServiceClient;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountServiceClient)
        {
            _discountServiceClient = discountServiceClient ?? throw new ArgumentNullException(nameof(discountServiceClient));
        }

        public async Task<CouponModel> GetDiscount(string productId)
        {
            var discountRequest = new GetDiscountRequest { ProductId = productId };
            return await _discountServiceClient.GetDiscountAsync(discountRequest);
        }
    }
}
