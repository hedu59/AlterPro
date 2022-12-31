using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Prototype.Shared.Commands;
using System;

namespace Prototype.Application.Commands.Input.Invitation
{
    public class UpdateInvitationCommand: Notifiable, IRequest<ICommandResult>
    {
        public Guid InvitationId { get; set; }
        public bool Status { get; set; }
        public bool Validate()
        {
            AddNotifications(new Contract()
            .Requires()
            .IsNotNull(InvitationId, "InvitationId", "The InvitationId can not be null")
            .IsNotNull(Status, "Status", "The State can not be null"));
            return Valid;
        }
    }
}
