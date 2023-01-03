﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prototype.Application.Commands.Input.Invitation;
using Prototype.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace Prototype.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]

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
        [Route("GetAvailable")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int? pageIndex, int? pageSize)
       => Ok( await _invitationService.GetInvitationsAvailablePagedAsync(pageIndex?? 1, pageSize ?? 10));


        [HttpGet]
        [Route("GetAceppted")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAceppted(int? pageIndex, int? pageSize)
       => Ok(await _invitationService.GetInvitationsAvailablePagedAsync(pageIndex ?? 1, pageSize ?? 10, true));

        [HttpPut]
        [AllowAnonymous]
        [Route("UpdateInvitation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateInvitation(long invitationId, bool status)
        {
            try
            {
                var result = await _mediator.Send(new UpdateInvitationCommand { InvitationId = invitationId, Status = status });

                if (result.Success) return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CreateInvitationCommand command)
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
