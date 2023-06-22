using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talabat.BL.Interfaces;
using Talabat.BL.Specifications.Product_Specification;
using Talabat.DAL.Entities;
using Talabat.DTOs;
using Talabat.Errors;
using Talabat.Helpers;

namespace Talabat.Controllers
{
   
    public class ProductController : BaseApiController
    {
        private readonly IGenericRepo<Product> productsRepo;
        private readonly IGenericRepo<ProductBrand> brandsRepo;
        private readonly IGenericRepo<ProductType> typesRepo;
        private readonly IMapper mapper;

        public ProductController(IGenericRepo<Product> productsRepo , IGenericRepo<ProductBrand> brandsRepo , IGenericRepo<ProductType> TypesRepo, IMapper mapper)
        {
            this.productsRepo = productsRepo;
            this.brandsRepo = brandsRepo;
            typesRepo = TypesRepo;
            this.mapper = mapper;
        }

        #region comments
        //ActionResult vs IActionResult 
        //IActionResult -- mvc دا الى هوا ممكن ترجع اى حاجه...ودى يفضل استخدمها مع ال general دى مينفعش تاخد هيا هترجع اى ف بتبقا  
        //ActionResult -- api هنا هينفع احدد نوع الحاه الى انا عايز ارجعها جواها...ودى بيفضل اكتر استخدمها مع ال

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        //swagger بتاعه ال documentation دول معمولين عشان احسن من ال
        //response ودا بيبقا فيها اى هيا اشكال ال Responsesبيبقا فيه لكل واحده منهم حاجه اسمها swagger عل ال api لان قبل م اجرب ال
        //الى ممكن ترجع الى هوا انتا قبل متجربها اصلا تبقا عارف اى الى ممكن برجع
        //ok ال response ف من غير م عرفلو الاتتين دول هوا مش بيبقا ظاهر غير ال
        //notfound,ok لكن لما اعرفلو كدا مثلا هيحجيبلى اتنين مش واحد وهما 

        //ProductSpecParams -- clean code دا انا عاملو عشان البراميترز كترت عن 3 ف الفانكشن دى ف عملتلهم كلاس وبعتهم هنا ف دا افضل ك 
        #endregion
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductWithTypeAndBrandSpecification(productParams);

            var products =await productsRepo.GeAllWithSpecAsync(spec);

            if (products == null) return NotFound(new ApiResponse(404));

            var data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            #region comments
            //ProductWithFiltersForCountSpecification --> دلوقتى هوا انا عملت دا لى
            //BrandId=2 انو يجيب 3 منتجات بس من المنتجات الى عندها ال Pagination هنفترض ان انا عملت ريكوست ب
            // هيبقا فيه الاتى Pagination فكدا كلاس ال 
            //pageIndex = 0 لانى انا مبعتهاش فهتبقى بصفر
            //PageSize=3
            //count --> BrandId=2  ودا عدد المنتجات نفسها الى موجوده عندى ف الداتابيز وعندها ال
            //data ودى بيانات ال 3 منتجات الى انا مرجعهم
            //BrandId=2  انا عايز اجيب عدد المنتجات عموما الى عندها ال
            //Pagination لان دول 3 بس عشان معمول عليهم data.count ف انا مينفعش اتغابى واروح اعمل كدا
            //BrandId=2 الى احنا عايزين نحققها هي اننا نجيب المنتجات الى عندها query فعشان كدا عملنا دا عشان بس يبقا ال
            //count او اى حاجه ف كدا هيجيبلى عددهم الفعلى وبكدا ابقا جيب ال Pagination بدون اى 
            //GetCountAsync(CountSpec) -->تانيه SpecificationS الى هتجيب بيه بيانات المنتجات بس ومش هيبقا فيه اى brandId فكدا دي هيبعتلها بس 
            #endregion
            var CountSpec = new ProductWithFiltersForCountSpecification(productParams);

            var Count = await productsRepo.GetCountAsync(CountSpec);

            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex , productParams.PageSize , Count , data));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
            var spec = new ProductWithTypeAndBrandSpecification(id);

            var product = await productsRepo.GetByIdWithSpecAsync(spec);

            if (product == null) return NotFound(new ApiResponse(404));

            return Ok(mapper.Map<Product, ProductToReturnDto>(product));
        }


        [HttpGet("brands")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await brandsRepo.GeAllAsync();

            if (brands == null) return NotFound(new ApiResponse(404));

            return Ok(brands);
        }


        [HttpGet("types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var types = await typesRepo.GeAllAsync();

            if (types == null) return NotFound(new ApiResponse(404));

            return Ok(types);
        }

    }
}
