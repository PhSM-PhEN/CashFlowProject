using CashFlow.Application.UseCases.ToUser.ChangePassword;
using CashFlow.Application.UseCases.ToUser.Delete;
using CashFlow.Application.UseCases.ToUser.GetUser;
using CashFlow.Application.UseCases.ToUser.Register;
using CashFlow.Application.UseCases.ToUser.Update;
using CashFlow.Communication.Request.ToUser;
using CashFlow.Communication.Responses;
using CashFlow.Communication.Responses.ToUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterUserUseCase useCase,
            [FromBody] RequestRegisterUserJson request)
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseUserProfile), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> Get(
            [FromServices] IGetUserUseProfileCase useCase)
        {
            var resposne = await useCase.Execute();

            return Ok(resposne);
        }
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdateProfile(
            [FromServices] IUpdateProfileUseCase useCase, 
            [FromBody] RequestUpdateUserJson request)
        {

            await useCase.Execute(request);
            return NoContent();

        }
        [HttpPut("passwordChang")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword([FromServices] IChangePasswordUseCase useCase,
            [FromBody] RequestChangePasswordJson request)
        {
            await useCase.Execute(request);
            return NoContent();
        }
        [HttpDelete]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeletePorfile([FromServices] IDeleteUserAccountUseCase useCase)
        {
            await useCase.Execute();
            return NoContent();
        }
    }
}
