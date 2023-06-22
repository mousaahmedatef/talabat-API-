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
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            #region Comments
            //( OwnsMany()كانت هتبقا  list يعني لانو لو list مش) ItemOrdered عندو بروبيرتى واحده اسمها  OrderItem واحد ف الداتابيز ف انا بقولو جدول ال table هنا ف السطر دا انا بجمع كلاسين ف 
            //وكدل يبقا جمعناهم ف جدول واحد OrderItem الى فيه ف جدول ال properties ودى عباره عن كلاس تاني ف حطلى ال
            #endregion
            builder.OwnsOne(item => item.ItemOrdered, product => product.WithOwner());

            #region Comments
             //.HasColumnType("decimal(18,2)"); ==> دى معناها مكون من 18 رقم منهم اتنين بعد العلامه
            #endregion
            builder.Property(item => item.Price)
                .HasColumnType("decimal(18,2)"); 
        }
    }
}
