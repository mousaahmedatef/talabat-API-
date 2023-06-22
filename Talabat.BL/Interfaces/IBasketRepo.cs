using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.DAL.Entities;

namespace Talabat.BL.Interfaces
{
    public interface IBasketRepo
    {
        Task<CustomerBasket> GetCustomerBasket(string basketId);
        Task<CustomerBasket> UpdateCustomerBasket(CustomerBasket basket);
        Task<bool> DeleteCustomerBasket(string basketId);
    }
}
