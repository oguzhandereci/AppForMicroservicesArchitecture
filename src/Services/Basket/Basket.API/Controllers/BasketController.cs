using Basket.API.Entities;
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

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
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
            return await _basketRepository.UpdateBasket(basket);
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
