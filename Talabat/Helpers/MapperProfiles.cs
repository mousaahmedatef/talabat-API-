using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talabat.DAL.Entities;
using Talabat.DAL.Entities.Identity;
using Talabat.DAL.Entities.Order_Aggregate;
using Talabat.DTOs;

namespace Talabat.Helpers
{
    public class MapperProfiles:Profile
    {
        public MapperProfiles()
        {
            #region Comments
            //d stands for-> Destination -> ProductToReturnDto
            //s stands for-> source -> Product
            //o => o.MapFrom()-->بتاعى ياخدها Destination بحط فيها القيمه الى انا عايز ال
            #endregion
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<DAL.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<AddressDto , DAL.Entities.Order_Aggregate.Address>();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryCost, o => o.MapFrom(s => s.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.PrductId, o => o.MapFrom(s => s.ItemOrdered.PrductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PictureUrl))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());

        }
    }
}
