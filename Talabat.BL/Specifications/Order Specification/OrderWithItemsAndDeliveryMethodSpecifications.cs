using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.BL.Repositories.Specifications;
using Talabat.DAL.Entities.Order_Aggregate;

namespace Talabat.BL.Specifications.Order_Specification
{
    public class OrderWithItemsAndDeliveryMethodSpecifications : BaseSpecification<Order>
    {
        // this constructor is used for get all orders for specific user
        public OrderWithItemsAndDeliveryMethodSpecifications(string buyerEmail):base(o=>o.BuyerEmail == buyerEmail)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);
            AddOrderByDescending(o => o.OrderDate);
        }

        // this constructor is used for get specific order for specific user
        public OrderWithItemsAndDeliveryMethodSpecifications(int orderId , string buyerEmail) : base(o => o.BuyerEmail == buyerEmail)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);
        }
    }
}
