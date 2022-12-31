using Prototype.Application.Interfaces;
using Prototype.Domain.Entities;
using Prototype.Domain.Enums;
using Prototype.Domain.Interfaces.IUnitOfWork;
using Prototype.Domain.Interfaces.IUnitOfWork.Collections;
using System;
using System.Threading.Tasks;

namespace Prototype.Application.Services
{
    public class DocumentoService : IDocumentoService
    {

        private readonly IUnitOfWork _uow;

        public DocumentoService( IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IPagedList<Documento>> ObterListDeDocumento(int pageIndex, int pageSize)
        {
            try
            {
                var documentos = await _uow.GetRepository<Documento>()
                                     .GetPagedListAsync(predicate: x => x.Active == true, 
                                     disableTracking: true, 
                                     pageIndex: pageIndex, 
                                     pageSize: pageSize);

                documentos = ConverterCategoria(documentos);

                return documentos;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<IPagedList<Documento>> ObterListDeDocumentoPorServidor(Guid ServidorId, int pageIndex, int pageSize)
        {
            try
            {
                var documentos = await _uow.GetRepository<Documento>().GetPagedListAsync(
                   predicate: x => x.ServidorId == ServidorId &&
                   x.Active == true,
                   disableTracking: true,
                   pageIndex: pageIndex, pageSize: pageSize);

                documentos = ConverterCategoria(documentos);

                return documentos;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Documento> ObterDocumentoPorID(Guid Id)
        {
            var documento = await _uow.GetRepository<Documento>().GetFirstOrDefaultAsync(
                 predicate: x => x.Id == Id && x.Active,
                 disableTracking: true);

            return documento;
        }


        private IPagedList<Documento> ConverterCategoria(IPagedList<Documento> documentos)
        {

            foreach (var item in documentos.Items)
            {
                switch (item.Categoria)
                {
                    case ECategoriaDocumento.Identificacao:
                        item.CategoriaDescicao = "Identificacao";
                        break;
                    case ECategoriaDocumento.VidaFuncional:
                        item.CategoriaDescicao = "Vida Funcional";
                        break;
                    case ECategoriaDocumento.ContagemDeTempo:
                        item.CategoriaDescicao = "Contagem De Tempo";
                        break;
                    case ECategoriaDocumento.Remuneracao_Proventos:
                        item.CategoriaDescicao = "Remuneracao de Proventos";
                        break;
                    default:
                        break;

                }

            }
            return documentos;
        }

    }
}
