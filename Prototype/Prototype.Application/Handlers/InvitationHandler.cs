using Flunt.Notifications;
using MediatR;
using Prototype.Application.Commands.Input.Invitation;
using Prototype.Domain.Commands.Output;
using Prototype.Domain.Entities;
using Prototype.Domain.Interfaces.IUnitOfWork;
using Prototype.Domain.ValueObjects;
using Prototype.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Prototype.Application.Handlers
{
    public class InvitationHandler : Notifiable,
        IRequestHandler<CreateInvitationCommand, ICommandResult>,
        IRequestHandler<UpdateInvitationCommand, ICommandResult>

    {
        private readonly IUnitOfWork _uow;
        public InvitationHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ICommandResult> Handle(UpdateInvitationCommand command, CancellationToken cancellationToken)
        {
            try
            {
                command.Validate();

                if (!command.Valid) return new CommandResult(success: false, message: "Error to update Invitation", data: command.Notifications);

                Invitation invitation = await _uow
                    .GetRepository<Invitation>()
                    .GetFirstOrDefaultAsync(predicate: x => x.Id == command.InvitationId);

                if (invitation == null) return new CommandResult(success: false, message: "Invitation not found", data: command);

                invitation.UpdateStatus(invitation.Price, command.Status);

                _uow.GetRepository<Invitation>().Update(entity: invitation);
                _uow.SaveChanges();

                return new CommandResult(success: true, message: "Invitation updated successfully", data: null);
            }
            catch (Exception ex)
            {

                return new CommandResult(success: false, message: $"Error to update Invitation {ex.Message}", data: ex);
            }


        }

        public async Task<ICommandResult> Handle(CreateInvitationCommand command, CancellationToken cancellationToken)
        {
            try
            {
                command.Validate();
                if (!command.Valid) return new CommandResult(success: false, message: "Error to create Invitation", data: command.Notifications);

                var address = Address.Create(command.Number, command.Street, command.Complement, command.Neighborhood, command.City, command.State, command.PostalCode);
                var email = Email.Create(command.Email);
                var contact = Contact.Create(command.FullName, command.PhoneNumber);
                var invitation = Invitation.Create(contact, address, email, command.Category, command.Price, command.Description, null);

                using (var uow = _uow)
                {
                    uow.GetRepository<Contact>().Save(contact);
                    uow.GetRepository<Invitation>().Save(invitation);
                    uow.SaveChanges();
                }

                return await Task.FromResult(new CommandResult(success: true, message: "Invitation created successfully", data: default));
            }
            catch (Exception ex)
            {

                return new CommandResult(success: false, message: $"Error to create Invitation {ex.Message}", data: ex);
            }
        }
    }
}
