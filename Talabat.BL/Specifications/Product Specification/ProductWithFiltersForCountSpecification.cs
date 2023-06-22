using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.BL.Repositories.Specifications;
using Talabat.DAL.Entities;

namespace Talabat.BL.Specifications.Product_Specification
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams productParams)
           : base(P =>
           (string.IsNullOrEmpty(productParams.Search) || P.Name.ToLower().Contains(productParams.Search)) &&
           (!productParams.TypeId.HasValue || P.ProductTypeId == productParams.TypeId.Value) &&
           (!productParams.BrandId.HasValue || P.ProductBrandId == productParams.BrandId.Value)
           )
        { }
    }
}
