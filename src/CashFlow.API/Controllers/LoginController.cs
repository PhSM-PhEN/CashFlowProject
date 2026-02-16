using CashFlow.Application.UseCases.ToLogin;
using CashFlow.Communication.Request.ToLogin;
using CashFlow.Communication.Responses;
using CashFlow.Communication.Responses.ToUser;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]

        public async Task<ActionResult> Login(
            [FromServices] IDoLoginUseCase doLoginUseCase,
            [FromBody] RequestLoginJson request)
        {
            var response = await doLoginUseCase.Execute(request);

            return Ok(response);

        }
    }
}
