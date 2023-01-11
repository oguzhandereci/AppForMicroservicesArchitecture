using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcService _discountGrpcService;

        public BasketController(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService)
        {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
            _discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
        }

        //GetBasket(with all items)
        [HttpGet("{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //gönderilen username bilgisi için validasyon kuralı işletilip, validasyondan geçmediği durumlarda bu status code döndürülebilir.
        public async Task<ActionResult<ShoppingCart>> GetBasket(string username)
        {
            return Ok(await _basketRepository.GetBasket(username) ?? new ShoppingCart(username));
        }
        //UpdateBasket(Add or Remove Item)
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//gönderilen basket nesnesi için validasyon kuralı işletilip, validasyondan geçmediği durumlarda bu status code döndürülebilir.
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            foreach (var item in basket.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductId);

                if (coupon is not null)
                {
                    item.Price -= coupon.CouponAmount;
                }
            }

            return Ok(await _basketRepository.UpdateBasket(basket));
        }
        //DeleteBasketCompletely
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //gönderilen username bilgisi için validasyon kuralı işletilip, validasyondan geçmediği durumlarda bu status code döndürülebilir.
        public async Task<ActionResult> DeleteBasket(string username)
        {
            await _basketRepository.DeleteBasket(username);
            return Ok();
        }
    }
}
