using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talabat.BL.Interfaces;
using Talabat.DAL.Entities;
using Talabat.DTOs;

namespace Talabat.Controllers
{
    
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepo _basketRepo;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepo basketRepo , IMapper mapper)
        {
            _basketRepo = basketRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string basketId)
        {
            var basket = await _basketRepo.GetCustomerBasket(basketId);
            return Ok(basket ?? new CustomerBasket(basketId));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var customerBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var updatedBasket = await _basketRepo.UpdateCustomerBasket(customerBasket);
            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket (string basketId)
        {
            return await _basketRepo.DeleteCustomerBasket(basketId);
        }

    }
}
