﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prototype.Application.Commands.Input.Invitation;
using Prototype.Application.Commands.Input.Servidores;
using Prototype.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace Prototype.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationController : ControllerBase
    {

        public readonly IInvitationService _invitationService;
        public readonly IMediator _mediator;

        public InvitationController(IInvitationService invitationService, IMediator mediator)
        {
            _invitationService = invitationService;
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int? pageIndex, int? pageSize)
       => Ok( await _invitationService.GetInvitationsPagedAsync(pageIndex?? 1, pageSize ?? 10));


        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> Put([FromBody] UpdateInvitationCommand command)
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
