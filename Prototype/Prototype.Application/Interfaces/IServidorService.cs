using Prototype.Domain.Entities;
using Prototype.Domain.Interfaces.IUnitOfWork.Collections;
using System;
using System.Threading.Tasks;

namespace Prototype.Application.Interfaces
{
    public interface IServidorService
    {
        Task<Servidor> ObterTramitacoesPorID(long Id);
        Task<IPagedList<Servidor>> ObterServidores(int pageIndex, int pageSize);
    }
}
