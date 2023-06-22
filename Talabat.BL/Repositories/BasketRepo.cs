using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.BL.Interfaces;
using Talabat.DAL.Entities;

namespace Talabat.BL.Repositories
{
    public class BasketRepo : IBasketRepo
    {
        private readonly IDatabase _database;

        public BasketRepo(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteCustomerBasket(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }
         
        public async Task<CustomerBasket> GetCustomerBasket(string basketId)
        {
            var basket = await _database.StringGetAsync(basketId);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket> UpdateCustomerBasket(CustomerBasket basket)
        {
            #region Comments
            //الى مبعوت basket Idبنفس ال create ولو ملقتهاش اصلا اعملها basket السطر الى تحتى دا بيقولو عدل بيانات ال
            // فنفس الوقت update و create فهو بيعمل
            #endregion
            var created =await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
            if (!created) return null;
            return await GetCustomerBasket(basket.Id);
        }
    }
}
