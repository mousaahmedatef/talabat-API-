using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talabat.DAL.Entities.Order_Aggregate;

namespace Talabat.DTOs
{
    public class OrderToReturnDto
    {
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public Address ShipToAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal DeliveryCost { get; set; }
        public string Status { get; set; }
        public List<OrderItemDto> Items { get; set; }
        public int PaymentIntentId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}
