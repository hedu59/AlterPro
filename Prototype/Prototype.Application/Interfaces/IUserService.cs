using Prototype.Application.Commands.Input.User;
using Prototype.Shared.Commands;
using System;
using System.Threading.Tasks;

namespace Prototype.Application.Interfaces
{
    public interface IUserService
    {
        Task<ICommandResult> AuthenticationUser(string login, string password, string email);

        ICommandResult CreateUserDefault();

    }
}
