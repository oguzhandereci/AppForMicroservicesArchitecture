using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Entities
{
    public class Coupon
    {
        public int CouponId { get; set; }

        [StringLength(24,ErrorMessage = "ProductId bilgisi yanlış.")]
        public string ProductId { get; set; }
        public string Description { get; set; }
        public int CouponAmount { get; set; }
    }
}
