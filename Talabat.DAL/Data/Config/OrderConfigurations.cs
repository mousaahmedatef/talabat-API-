using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.DAL.Entities.Order_Aggregate;

namespace Talabat.DAL.Data.Config
{
    public class OrderConfigurations: IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShipToAddress, Address => Address.WithOwner());
            builder.Property(O => O.Status)
                .HasConversion(
                OStatus => OStatus.ToString(),
                OStatus =>(OrderStatus) Enum.Parse(typeof(OrderStatus), OStatus));
            #region comments
            //انا بعرفو هتروح ازاى للداتابيز وهستقبلها ازاى من الداتابيز Status هنا فال 
            // string فهيا هتروح ك 
            //طبعا من الداتابيز string ولما اجى استقبلها هستقبلها ك 
            //EnumMember الى اسمها data annotation كدا الى موجوده ف ال pending زى Enum الى هوا موجود ف ال enum member ل string وبعدين هحولها من 
            //عشان (OrderStatus) الى هيا صفر او واحد ب دا enum هحولها لقيمتها الاصليه ف ال EnumMember وبعد م حولتها ل OrderStatus enum الى جوا ال
            // عشان تبقا اسهل لبتاع الفرونت انو يتعامل معاها
            #endregion

            #region Comments
            // one to many الى موجود جوا الاوردر علاقه  itemsوال orderهنا انابقولو ان العلاقه بين ال
            //builder.HasMany(O => O.Items).WithOne() بس هوا عرفها تلقائى من غير م اعمل السطر دا
            //وهيا configurations ولكن انا كتبتها عشان هعمل عل العلاقه نفسها شويه   
            // الى ف الاوردر دا او الى ليها علاقه بيه  items انو لمالا يروح يسمح الاوردر يروح يمسح معاه كل ال
            #endregion
            builder.HasMany(O => O.Items).WithOne().OnDelete(DeleteBehavior.Cascade);


        }
    }
}
