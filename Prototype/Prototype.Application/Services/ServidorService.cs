using Microsoft.EntityFrameworkCore;
using Prototype.Application.Interfaces;
using Prototype.Domain.Entities;
using Prototype.Domain.Enums;
using Prototype.Domain.Interfaces.IUnitOfWork;
using Prototype.Domain.Interfaces.IUnitOfWork.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prototype.Application.Services
{
    public class ServidorService : IServidorService
    {
        private readonly IUnitOfWork _uow;

        public ServidorService( IUnitOfWork uow)
        {

            _uow = uow;
        }

        public async Task<IPagedList<Servidor>> ObterServidores(int pageIndex, int pageSize)
        {
            var servidores =  await _uow.GetRepository<Servidor>()
                .GetPagedListAsync(predicate: x => x.Active == true,  pageIndex: pageIndex, pageSize: pageSize,
                include: x => x.Include(y => y.Documentos));

            return servidores;
        }

        public async Task<Servidor> ObterTramitacoesPorID(Guid Id)
        {
            
            var servidor = await _uow.GetRepository<Servidor>().GetFirstOrDefaultAsync(
               predicate: x => x.Id == Id && x.Active,
               disableTracking: true);

            var tramitacao = await _uow.GetRepository<ProcessoTramitacao>()
                                .GetAsync(predicate: x => x.ServidorId == Id && x.Active, disableTracking: true);

            tramitacao = ConverterSetores(tramitacao.ToList()).AsQueryable();
            servidor.SetorDescricao = tramitacao.LastOrDefault().SetorDestinoDescricao;
            servidor.SetorTramitacao = tramitacao.LastOrDefault().SetorDestino;

            servidor.Tramitacoes = tramitacao;

            return servidor;
        }

        private List<ProcessoTramitacao> ConverterSetores(List<ProcessoTramitacao> tramitacao)
        {
            foreach (var item in tramitacao)
            {
                switch (item.SetorOrigem)
                {
                    case ESetoresTramitacao.Setorial_Servidor:
                        item.SetorOrigemDescricao = "Setorial Servidor";
                        break;
                    case ESetoresTramitacao.CPrev_Gestor:
                        item.SetorOrigemDescricao = "CPrev Gestor";
                        break;
                    case ESetoresTramitacao.Secretario_SEPLAG:
                        item.SetorOrigemDescricao = "Secretario SEPLAG";
                        break;
                    case ESetoresTramitacao.PGE_Analise:
                        item.SetorOrigemDescricao = "PGE Analise";
                        break;
                    case ESetoresTramitacao.TCE_Gestor:
                        item.SetorOrigemDescricao = "TCE Gestor";
                        break;
                    default:
                        break;
                }

                switch (item.SetorDestino)
                {
                    case ESetoresTramitacao.Setorial_Servidor:
                        item.SetorDestinoDescricao = "Setorial Servidor";
                        break;
                    case ESetoresTramitacao.CPrev_Gestor:
                        item.SetorDestinoDescricao = "CPrev Gestor";
                        break;
                    case ESetoresTramitacao.Secretario_SEPLAG:
                        item.SetorDestinoDescricao = "Secretario SEPLAG";
                        break;
                    case ESetoresTramitacao.PGE_Analise:
                        item.SetorDestinoDescricao = "PGE Analise";
                        break;
                    case ESetoresTramitacao.TCE_Gestor:
                        item.SetorDestinoDescricao = "TCE Gestor";
                        break;
                    default:
                        break;
                }
            }

            return tramitacao;
        }

    }
}
