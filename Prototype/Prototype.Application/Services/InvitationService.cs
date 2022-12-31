using Microsoft.EntityFrameworkCore;
using Prototype.Application.Interfaces;
using Prototype.Domain.Entities;
using Prototype.Domain.Interfaces.IUnitOfWork;
using Prototype.Domain.Interfaces.IUnitOfWork.Collections;
using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task<IPagedList<Invitation>> GetInvitationsPagedAsync(int pageIndex, int pageSize)
        {
            var servidores = await _uow.GetRepository<Invitation>()
                .GetPagedListAsync(predicate: x => x.Active == true, pageIndex: pageIndex, pageSize: pageSize,
                include: x => x.Include(y => y.Contact));

            return servidores;
        }
    }
}
