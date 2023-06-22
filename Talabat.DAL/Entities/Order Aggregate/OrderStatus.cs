using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.DAL.Entities.Order_Aggregate
{
    public enum OrderStatus
    {
        #region comments
        //[EnumMember(Value = "Pending")] بصفر وانا مش عايز اخزنها كدا او مش عايز اخزنها ارقام pending هنا انا عامل دى لان هنا قيمه ال
        //Pending فعشان كدا كتب دا وهوا بدل مبروح يخزن صفر هيخزن كلمه
        #endregion
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Payment Received")]
        PaymentReceived,
        [EnumMember(Value = "Payment Failed")]
        PaymentFailed
    }
}
