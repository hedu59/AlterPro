using Prototype.Domain.Entities;
using Prototype.Domain.Interfaces.IUnitOfWork.Collections;
using System;
using System.Threading.Tasks;

namespace Prototype.Application.Interfaces
{
    public interface IDocumentoService
    {

        Task<IPagedList<Documento>> ObterListDeDocumento(int pageIndex, int pageSize);

        Task<IPagedList<Documento>> ObterListDeDocumentoPorServidor(Guid ServidorId, int pageIndex, int pageSize);

        Task<Documento> ObterDocumentoPorID(Guid Id);

    }
}
