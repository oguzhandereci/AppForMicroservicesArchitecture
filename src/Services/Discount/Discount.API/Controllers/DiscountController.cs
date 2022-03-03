using Discount.API.Entities;
using Discount.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
        }

        [HttpGet("GetCouponForDiscount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//gönderilen basket nesnesi için validasyon kuralı işletilip, validasyondan geçmediği durumlarda bu status code döndürülebilir.
        public async Task<ActionResult<Coupon>> GetCouponForDiscount(string productId)
        {
            return await _discountRepository.GetCouponForDiscount(productId);
        }

        [HttpPost("AddCoupon")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//gönderilen basket nesnesi için validasyon kuralı işletilip, validasyondan geçmediği durumlarda bu status code döndürülebilir.
        public async Task<ActionResult<Coupon>> AddCoupon([FromBody] Coupon coupon)
        {
            bool haveAnyAddedItems = await _discountRepository.InsertCoupon(coupon);
            return haveAnyAddedItems ? CreatedAtRoute("GetCouponForDiscount", new { productId = coupon.ProductId }, coupon) : BadRequest(new { errorDesc = "The process of adding coupon was unsucessful." });
        }

        [HttpPost("UpdateCoupon")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//gönderilen basket nesnesi için validasyon kuralı işletilip, validasyondan geçmediği durumlarda bu status code döndürülebilir.
        public async Task<ActionResult<Coupon>> UpdateCoupon([FromBody] Coupon coupon)
        {
            bool haveAnyAddedItems = await _discountRepository.UpdateCoupon(coupon);
            return haveAnyAddedItems ? CreatedAtRoute("GetCouponForDiscount", new { productId = coupon.ProductId }, coupon) : BadRequest(new { errorDesc = "The process of updating coupon was unsucessful." });
        }

        [HttpPost("DeleteCoupon")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//gönderilen basket nesnesi için validasyon kuralı işletilip, validasyondan geçmediği durumlarda bu status code döndürülebilir.
        public async Task<ActionResult<Coupon>> DeleteCoupon(string productId)
        {
            bool haveAnyAddedItems = await _discountRepository.DeleteCoupon(productId);
            return haveAnyAddedItems ? Ok($"The coupon of product which has Id:{productId} has successfully deleted.") : BadRequest("The process of deleting coupon was unsucessful.");
        }
    }
}
