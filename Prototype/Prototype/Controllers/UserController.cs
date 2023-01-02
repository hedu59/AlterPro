using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prototype.Application.Commands.Input.User;
using Prototype.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace Prototype.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IMediator _mediator;
        public UserController(IUserService userService, IMediator mediator )
        {
            _userService = userService;
            _mediator = mediator;
        }

    
        [AllowAnonymous]
        [HttpPost("CreateUserDefault")]
        public IActionResult CreateUserDefault()
        {

            var validar = _userService.CreateUserDefault();
            return Ok(validar);
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                if (result.Success) return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] UpdateUserCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                if (result.Success) return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}