using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.BL.Repositories.Specifications;
using Talabat.DAL.Entities;

namespace Talabat.BL.Specifications.Product_Specification
{
    public class ProductWithTypeAndBrandSpecification:BaseSpecification<Product>
    {
        #region comments
        //وكذلك بعمل كدا مع ى كلاس عايز وانا بجيب بياناتو Product بتاعه ال includes الكلاس دا انا عاملو عشان اضيف من خلاله ال
        //بتاعتو includes اجيب معاه ال
        #endregion

        #region comments
        //:base(P=>
        //   (!typeId.HasValue || P.ProductTypeId == typeId.Value)&&
        //   (!brandId.HasValue || P.ProductBrandId == brandId.Value)
        //   ) 

        //filteration بتاعه ال Criteriaانا هنا ببعت ال
        //where دى فكرتها اى ؟ فكرتها ان انا عايز اعمل فلتر على الداتا وطبعا الفتر عندان بيتعمل باستخدم ال
        //الى  هيا المفروض فلتر Criteria ؤ بياخد ال constructor فيه  BaseSpecification ف انا عندى ف ال
        //جاي بقيمه يبقا كدا هفلتر بيه فبخلى اول كوندشن بفولس عشان يخش ف التاني ويبعتو typeId ف انا ببعتلو بقولو لو ال
        //P.ProductTypeId == typeId.Value لان هوا لو لقي واحد بترو مش هيخش ف التانى ف انا بخليه بفولس عشان يبعت
        // not false = true لكن لو مش جاي بقيمه يبقا كدا انا مش محتاج افلتر بيه ف كدا لو الكوندشن هيبقا فولس و
        //P.ProductTypeId == typeId.Value فبالتالى لو شرط منهم تحقق مش هيخش ف التاني فكدا هنامش هيخش ف التاني ف مش هيبعت كدا
        #endregion
        public ProductWithTypeAndBrandSpecification(ProductSpecParams productParams)
            :base(P=>
            (string.IsNullOrEmpty( productParams.Search) || P.Name.ToLower().Contains(productParams.Search))&&
            (!productParams.TypeId.HasValue || P.ProductTypeId == productParams.TypeId.Value)&&
            (!productParams.BrandId.HasValue || P.ProductBrandId == productParams.BrandId.Value)
            ) 
        {

            AddInclude(P => P.ProductType);
            AddInclude(P => P.ProductBrand);
            AddOrderBy(p => p.Name);

            ApplyPaginaton(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        break;
                }
            }
        }

        public ProductWithTypeAndBrandSpecification(int id):base(P=>P.Id == id)
        {
            AddInclude(P => P.ProductType);
            AddInclude(P => P.ProductBrand);
        }
    }
}
