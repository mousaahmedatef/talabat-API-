using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Talabat.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }

        #region Comments
        // statuscode , message ودا بيختلف عن اى ايرور تاني ف ان اي ايرور مثلا انا هظهرلو ال ValidationError الكلاس دا معمول عشان ال
        // string وانا باعتو int المفروض يكون  id نفسها بقا الى هوا مثلا ال validations errors لكن هنا انا هظهرلو الاتنين دول زائد ال
        #endregion
        public ApiValidationErrorResponse():base(400)
        {

        }
    }
}
