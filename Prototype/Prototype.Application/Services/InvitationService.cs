using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Prototype.Application.Interfaces;
using Prototype.Application.Models;
using Prototype.Application.Profiles;
using Prototype.Domain.Commands.Output;
using Prototype.Domain.Entities;
using Prototype.Domain.Interfaces.IUnitOfWork;
using Prototype.Domain.Interfaces.IUnitOfWork.Collections;
using Prototype.Shared.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prototype.Application.Services
{
    public class InvitationService : IInvitationService
    {
        private readonly IUnitOfWork _uow;

        public InvitationService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ICommandResult> GetInvitationsAvailablePagedAsync(int pageIndex, int pageSize, bool? accepted = null)
        {
            var invitations = await _uow.GetRepository<Invitation>()
                .GetPagedListAsync(predicate: x => x.Active == true && x.Status == accepted, pageIndex: pageIndex, pageSize: pageSize,
                include: x => x.Include(y => y.Contact));
            
            if (invitations?.Items?.Count > 0)
            {
                var invitationsModel = GetInvitationModel(invitations);
                return new CommandResult(true, "Invitations found", invitationsModel);
            }

            return new CommandResult(false, "Invitations not found", null);
        }

        private PagedList<InvitationResult> GetInvitationModel(IPagedList<Invitation> invitations)
        {
            try
            {
                var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<InvitationProfile>();
                });

                var mapper = configuration.CreateMapper();

                var invitationsViewModel = new List<InvitationResult>();

                foreach (var item in invitations.Items)
                {
                    var invitationViewModel = new InvitationResult();
                    invitationViewModel = mapper.Map<InvitationResult>(item);
                    invitationsViewModel.Add(invitationViewModel);
                }


                var pagedListInvitations = new PagedList<InvitationResult>(invitationsViewModel, invitations.PageIndex, invitations.PageSize, invitations.IndexFrom);

                return pagedListInvitations;
            }
            catch (System.Exception)
            {
                return default;
            }
            
        }

    }
}
