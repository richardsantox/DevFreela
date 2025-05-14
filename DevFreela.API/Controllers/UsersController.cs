using DevFreela.Application.Commands.Login;
using DevFreela.Application.Commands.RecoveryPassword.ChangePassword;
using DevFreela.Application.Commands.RecoveryPassword.RequestRecoveryPassword;
using DevFreela.Application.Commands.RecoveryPassword.ValidateRecoveryCode;
using DevFreela.Application.Commands.Users.InsertUser;
using DevFreela.Application.Commands.Users.InsertUserSkill;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.Users.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(InsertUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
        }

        [HttpPost("{id}/skills")]
        public async Task<IActionResult> PostSkills(int id, InsertUserSkillCommand command)
        {
            command.IdUser = id;

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }

        [HttpPut("{id}/profile-picture")]
        public IActionResult PostProfilePicture(int id, IFormFile file)
        {
            var description = $"File: {file.FileName}, Size {file.Length}";

            return Ok(description);
        }

        [HttpPut("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("password-recovery/request")]
        [AllowAnonymous]
        public async Task<IActionResult> RequestRecoveryPassword(
            PasswordRecoveryRequestCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("password-recovery/validate")]
        [AllowAnonymous]
        public async Task<IActionResult> ValideteRecoveryCode(
            ValidateRecoveryCodeCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }

        [HttpPost("password-recovery/change")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword(
            ChangePasswordCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }
    }
}
