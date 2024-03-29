﻿using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            string serializedBasketObj = await _redisCache.GetStringAsync(userName);
            return !(serializedBasketObj is null) ? JsonConvert.DeserializeObject<ShoppingCart>(serializedBasketObj) : null;
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            //aynı key ile redis'e birden fazla ekleme işlemi yapar. İşlemi redis tool'da dene.
            await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.UserName);
        }

        public async Task DeleteBasket(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }

    }
}
