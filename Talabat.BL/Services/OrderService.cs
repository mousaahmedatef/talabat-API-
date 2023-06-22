using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.BL.Interfaces;
using Talabat.BL.Specifications.Order_Specification;
using Talabat.DAL.Entities;
using Talabat.DAL.Entities.Order_Aggregate;

namespace Talabat.BL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepo _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
      

        public OrderService(
            IBasketRepo basketRepo,
            IUnitOfWork unitOfWork
            ) 
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shipToAddress)
        {
            //1-Get Basket From Baskets Repo
            var basket =await _basketRepo.GetCustomerBasket(basketId);

            //2-Get Selected Items AT Basket From Products Repo
            var orderItems = new List<OrderItem>();

            foreach (var item in basket.Items)
            {
                var product =await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);

                var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);

                orderItems.Add(orderItem);
            }

            //3-Get Delivery Method From DeliveryMethods Repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            //4-Calculate SubTotal
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            //5-Create Order
            var order = new Order(buyerEmail, shipToAddress, deliveryMethod, orderItems, subTotal);

            await _unitOfWork.Repository<Order>().Add(order);

            //6-Save To Database [TODO]
            int result = await _unitOfWork.Complete();
            if (result <= 0) return null;

            return order;

        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrderWithItemsAndDeliveryMethodSpecifications(buyerEmail);
            var orders = await _unitOfWork.Repository<Order>().GeAllWithSpecAsync(spec);

            return orders;
        } 

        public async Task<Order> GetOrderByIdForUser(int orderId, string buyerEmail)
        {
            var spec = new OrderWithItemsAndDeliveryMethodSpecifications(orderId,buyerEmail);
            var order = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);

            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().GeAllAsync();
        }

        

        
    }
}
