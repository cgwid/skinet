using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {

        private readonly IDatabase _redis;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _redis = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _redis.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var data = await _redis.StringGetAsync(basketId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            // For update, not updating individual items but just updating the whole basket
            var created = await _redis.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), 
                TimeSpan.FromDays(30));

            if(!created) return null;

            return await GetBasketAsync(basket.Id);
        }
    }
}