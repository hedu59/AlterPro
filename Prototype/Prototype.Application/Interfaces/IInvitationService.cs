using Prototype.Shared.Commands;
using System.Threading.Tasks;

namespace Prototype.Application.Interfaces
{
    public interface IInvitationService
    {
        Task<ICommandResult> GetInvitationsAvailablePagedAsync(int pageIndex, int pageSize, bool? accepted = null);
    }
}
