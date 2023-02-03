using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;
        public BuggyController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest() 
        {
            var result = _context.Products.Find(42);
            if (result == null)
            {
                return NotFound();
            }

            return Ok();

        }

        [HttpGet("servererror")]
        public ActionResult GetServerErrorRequest() 
        {
            var result = _context.Products.Find(42);

            // Should error out here because result is null and can't call ToString
            // on something that doesn't exist
            var thingToReturn = result.ToString();

            return Ok();

        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequestRequest() 
        {
            return BadRequest();
        }

        // Validation error when you pass in a string instead of an int
        [HttpGet("badrequest/{id}")]
        public ActionResult GetValidationErrorRequest(int id) 
        {
            return Ok();
        }



    }
}