using Prototype.Domain.Entities;
using Prototype.Domain.Interfaces.IUnitOfWork.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Application.Interfaces
{
    public interface IInvitationService
    {
        Task<IPagedList<Invitation>> GetInvitationsPagedAsync(int pageIndex, int pageSize);
    }
}
