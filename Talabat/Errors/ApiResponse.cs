using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Talabat.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponse(int statusCode , string message = null)
        {
            this.StatusCode = statusCode;
            this.Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
            => statusCode switch
            {
                400 => "A Bad Request , You Have Made",
                401 => "Authorized , You Are Not",
                404 => "Resource Was Not Found",
                500 => "Erors Are Path To The Dark Side , Errors Lead To Anger , Anger Lead To Hate , Hate Leads To Career Change",
                _ => null
            };
    }
}
