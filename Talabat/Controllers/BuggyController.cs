using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talabat.DAL.Data;
using Talabat.Errors;

namespace Talabat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreContext context;

        public BuggyController(StoreContext context)
        {
            this.context = context;
        }

        

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var product = context.Products.Find(100);
            if (product == null) return NotFound(new ApiResponse(404));
            return Ok();

        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var product = context.Products.Find(100);
            var productToReturn = product.ToString();
            return Ok();

        }
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));

        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok();

        }
    }
}
