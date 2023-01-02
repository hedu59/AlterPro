using Prototype.Application.Filas.Models;

namespace Prototype.Application.Interfaces.Filas
{
    public  interface IEmailProducer
    {
        void ProduceEmail(InvitationMessage message);
    }
}
