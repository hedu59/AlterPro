using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Prototype.Application.Commands.Input.Invitation;
using Prototype.Application.Commands.Input.User;
using System.Reflection;

namespace Prototype.Api.ServicesResolve
{
    internal static class ResolveHandlers
    {
        internal static IServiceCollection AddHandlerDependency(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateUserCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(UpdateUserCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreateInvitationCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(UpdateInvitationCommand).GetTypeInfo().Assembly);

            return services;
        }
    }
}
