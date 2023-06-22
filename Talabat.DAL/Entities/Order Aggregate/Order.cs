using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.DAL.Entities.Order_Aggregate
{
    public class Order:BaseEntity
    {
        public Order()
        {
                
        }
        public Order(string buyerEmail, Address shipToAddress, DeliveryMethod deliveryMethod, List<OrderItem> items, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
        }

        public string BuyerEmail { get; set; }
        #region comments
        //DateTimeOffset==> am ولا pm دا بيعرفني اذا كان الوقت دا 
        #endregion
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public Address ShipToAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public List<OrderItem> Items { get; set; }
        public int PaymentIntentId { get; set; }
        #region Comments
        //SubTotal-->بتاعها price الى انا طلبتها فى ال  items دى عدد ال
        // هتبقى 3 *300 SubTotal يعنى لو انا اشتريت 3 ايربودز وسعر الواحده 300 فكدا ال
        #endregion
        public decimal SubTotal { get; set; }
        #region Comments
        //Total-->والخدمات والكلام دا deleviry زائد تكلفه ال  SubTotal دى بقا التكلفه الكامله الى هيدفعها العميل يعنى دى ال تكلفه المنتجات نفسها الى هيا
        #endregion
        public decimal GetTotal()
            => SubTotal + DeliveryMethod.Cost;
    }
}
