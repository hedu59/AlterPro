using MessageConsumer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessageConsumer.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(InvitationMessage invitation);
    }
}
