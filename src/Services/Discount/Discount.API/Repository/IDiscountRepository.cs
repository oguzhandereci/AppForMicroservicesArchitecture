using Discount.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Repository
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetCouponForDiscount(string productId);
        Task<bool> InsertCoupon(Coupon coupon);
        Task<bool> UpdateCoupon(Coupon coupon);
        Task<bool> DeleteCoupon(string productId);
    }
}
