using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Prototype.Domain.Enums;
using Prototype.Shared.Commands;
using System;

namespace Prototype.Application.Commands.Input.Documentos
{
    public class UpdateDocumentoCommand : Notifiable, IRequest<ICommandResult>
    {
        public long ServidorId { get; set; }
        public long DocumentoId { get; set; }
        public ESetoresTramitacao Tramitacao { get; set; }

        public bool Validate()
        {
            AddNotifications(new Contract()
            .Requires()
            .IsGreaterOrEqualsThan(ServidorId,1, "ServidorId", "O ServidorId não pode ser nulo")
            .IsGreaterOrEqualsThan(DocumentoId, 1,"DocumentoId", "O DocumentoId não pode ser nulo")

             );
            return Valid;

        }
    }
}
