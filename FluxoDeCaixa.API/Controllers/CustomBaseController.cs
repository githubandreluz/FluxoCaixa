using FluxoDeCaixa.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluxoDeCaixa.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomBaseController : ControllerBase
    {
        protected IActionResult returnToHttp(ResponseData response) =>
            returnToHttp<object>(response);

        protected IActionResult returnToHttp<T>(ResponseData<T> response)
        {
            if (response.HasErrors)
                return BadRequest(response.AllErrors);

            if (response.Response == null)
                return Ok();

            return Ok(response.Response);

        }
    }
}
