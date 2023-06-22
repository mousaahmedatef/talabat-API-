using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talabat.BL.Interfaces;
using Talabat.BL.Repositories;
using Talabat.BL.Services;
using Talabat.Errors;
using Talabat.Helpers;

namespace Talabat.Extentions
{
    public static class ApplicationServicesExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            #region comments
            //services.AddScoped<IGenericRepo<>, GenericRepo<>>();GenericRepo<Product> كدا type لانو لازم تحدد ال مثلاgeneric دا مش بيمشي مع الحاجات ال Syntax بس ال
            #endregion
            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            services.AddAutoMapper(typeof(MapperProfiles));
            services.AddScoped(typeof(ITokenService), typeof(TokenService));
            services.Configure<ApiBehaviorOptions>(options =>
            {
                #region comments
                //actionContext ==> endpoint بتاعه ال Configurations الىى شايل كل ال container دا ال
                // ModelState لان ممكن الاكشن يبقا ليها اكتر من   ModelStates بتاع الاكشن فيه  Context ف انا بقولو جوا ال
                //الى جواها ايرورز ModelStates ف انا بقولو هاتلى ال 
                //بس ErrorMessage وبعدين هاتلي الايرورز نفسها ف كل ايرور بيرجع ك اوبجيكت ف انا بقولو هاتلى من ااوبجيكت دا ال
                #endregion

                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState.Where(M => M.Value.Errors.Count > 0)
                                                         .SelectMany(M => M.Value.Errors)
                                                         .Select(E => E.ErrorMessage).ToArray();

                    var responseMessage = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(responseMessage);

                };
            });

            services.AddScoped(typeof(IBasketRepo), typeof(BasketRepo));
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;

        }
    }
}
