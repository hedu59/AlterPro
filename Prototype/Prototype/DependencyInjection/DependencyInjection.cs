using Microsoft.Extensions.DependencyInjection;
using Prototype.Application.Filas.Producers;
using Prototype.Application.Handlers;
using Prototype.Application.Interfaces;
using Prototype.Application.Interfaces.Filas;
using Prototype.Application.Services;
using Prototype.Domain.Interfaces.IUnitOfWork;
using Prototype.Infra.Data;
using Prototype.Infra.Data.Interfaces;
using Prototype.Infra.Data.UnitOfWork;
using Prototype.Shared.Auth;

namespace Prototype.Api.DependencyInjection
{
    public static class ResolveDependencyInjection
    {
 
        public static void ResolveDependencies(IServiceCollection services)
        {
            ServicesDependencies(services);
            HandlresDependecies(services);
            FilasDependencies(services);
        }

        static void FilasDependencies(IServiceCollection services)
        {
            services.AddScoped<IEmailProducer, EmailProducer>();
        }

        static void ServicesDependencies(IServiceCollection services)
        {
            services.AddScoped<IInvitationService, InvitationService>();
            services.AddScoped<IUnitOfWork, UnitOfWork<PrototypeDataContext>>();
            services.AddScoped<IUser, UserNet>();
        }

        static void HandlresDependecies(IServiceCollection services)
        {
            services.AddScoped<UserHandler, UserHandler>();
            services.AddScoped<InvitationHandler, InvitationHandler>();
        }
    }



}
