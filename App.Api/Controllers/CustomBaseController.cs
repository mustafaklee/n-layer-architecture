using App.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {

        [NonAction]
        public IActionResult CreateActionResult<T>(ServiceResult<T> response)
        {
            if (response.StatusCode == HttpStatusCode.NoContent)

                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode.GetHashCode(),
                };

            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode.GetHashCode()
            };


        }

        [NonAction]
        public IActionResult CreateActionResult(ServiceResult response)
        {
            if (response.StatusCode == HttpStatusCode.NoContent)

                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode.GetHashCode(),
                };

            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode.GetHashCode()
            };


        }


    }
}
