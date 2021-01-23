using System;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _datebase;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _datebase = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketID)
        {
            return await _datebase.KeyDeleteAsync(basketID);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var data = await _datebase.StringGetAsync(basketId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var created = await _datebase.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));

            if (!created) return null;

            return await GetBasketAsync(basket.Id);
        }
    }
}