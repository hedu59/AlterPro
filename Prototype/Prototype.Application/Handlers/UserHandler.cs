using Flunt.Notifications;
using MediatR;
using Prototype.Application.Commands.Input.User;
using Prototype.Domain.Commands.Output;
using Prototype.Domain.Entities;
using Prototype.Domain.Interfaces.IUnitOfWork;
using Prototype.Shared.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Prototype.Application.Handlers
{
    public class UserHandler : Notifiable,
         IRequestHandler<CreateUserCommand, ICommandResult>,
         IRequestHandler<UpdateUserCommand, ICommandResult>
    {
        private readonly IUnitOfWork _uow;

        public UserHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ICommandResult Handle()
        {
            try
            {
                _uow.GetRepository<User>()
                .Save(new User(email: "admin@admin.com", login: "admin", password: "123456"));

                _uow.SaveChanges();
                return new CommandResult(success: true, message: "Usuário salvo com sucesso", data: new User("admin", "123456", "admin@admin.com") { });
            }
            catch (Exception ex)
            {
                return new CommandResult(success: false, message: ex.Message, data: null);
            }
        }

        public async Task<ICommandResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                _uow.GetRepository<User>()
                .Save(new User(email: command.Email, login: command.Login, password: command.Password));

                _uow.SaveChanges();

                return await Task.FromResult( new CommandResult(success: true, message: "Usuário salvo com sucesso", data: command));
            }
            catch (Exception ex)
            {
                return new CommandResult(success: false, message: ex.Message, data: null);
            }
        }

        public async Task<ICommandResult> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _uow
                    .GetRepository<User>()
                    .GetFirstOrDefaultAsync(predicate: x => x.Id == command.UserId);

                if (user != null)
                {
                    user.UpdateUser(login: command.Login, email: command.Email, password: command.Password);

                    _uow.GetRepository<User>().Update(entity: user);

                    _uow.SaveChanges();

                    return await Task.FromResult(new CommandResult(success: true, message: "Usuário alterado com sucesso", data: command));
                }

                return await Task.FromResult(new CommandResult(success: false, message: "Usuário nao encontrada", data: command));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new CommandResult(success: false, message: ex.Message, data: null));
            }
        }
    }
}
