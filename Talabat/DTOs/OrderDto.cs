using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talabat.DAL.Entities.Order_Aggregate;

namespace Talabat.DTOs
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDto ShipToAddress { get; set; }
    }
}
