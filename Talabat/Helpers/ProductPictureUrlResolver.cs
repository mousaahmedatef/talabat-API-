using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talabat.DAL.Entities;
using Talabat.DTOs;

namespace Talabat.Helpers
{
    #region comments
    //ان انا عايز ونا برجع الداتا من لداتابيز عشان اعرضها لليوزر  resolver فكره ال
    //زي هنا مثلا الى انا عايز  لينك الصوره الى يجيلى من الداتابيز  Processing قبل م اعرضها اعمل عليها شويه شغل او 
    // بتاعى domain ادمج معاه عندى ال
    #endregion
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        public IConfiguration Configuration { get; }

        public ProductPictureUrlResolver(IConfiguration configration)
        {
            this.Configuration = configration;
        }
        
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{Configuration["ApiUrl"]}{source.PictureUrl}";
            return null;
        }
    }
}
