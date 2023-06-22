using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Errors;

namespace Talabat.Middelwares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        #region comments
        //RequestDelegate أل inject لازم تكون على الاقل بت middelware عشان ان اعرف ان دى 

        //طيب انا عاملها لى اصلا؟
        // 500 الى هوا ف ال  server error لل handel دى انا عاملها عشان اعمل
        //app.UseDeveloperExceptionPage(); كدا عن طريق error page ودى بتوديني على middelware بيروح ل  development وانا فمرحله ال exception المفروض انو لو كان حصل 
        // لكن انا مش عايز الاكسبشن يظهر بالطريقه دى انا عايزو يظهر بطريقه تانيه او بشكل تانى
        //دى middelware فعشان كدا عملت انا ال
        #endregion

        public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> logger , IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }
        #region Comments
        // Invoke دى فانكشن بتتنادى تلقائي ولازم اسميها
        #endregion
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                #region Comments
                //فلو فيه اكسبشن مش هيعرف يخش ف االى بعدها وهيخش ف الكاتش middelware هنا بقولو حاول تخش ف ال

                #endregion
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                #region Comments
                // الى انا عايزها تظهر Ex message هنا بقا المفروض انى انا هظبط ال

                #endregion
                logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";
                #region Comments
                //intس عباره عن context.Response.StatusCode انما enum عباره عن HttpStatusCode.InternalServerError لأن الى بيرجع من Casting هنا انا عملت 

                #endregion
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                var responseMessage = env.IsDevelopment()
                    ? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
                #region Comments
                //عشان بتاع الفرونت يعرف يتعامل معاها CamelCase الى انا هرجعو تبقا json جوا ال property هنا انا عايز كل 

                #endregion
                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(responseMessage , options); //json بتاخد  WriteAsync عشان 
                await context.Response.WriteAsync(json);
            }
        }

    }
}
