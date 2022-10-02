using MessageConsumer.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessageConsumer.Infra
{
    public interface ITransacaoMongoRepository
    {
        Task<List<LogTransacao>> GetAllAsync();
        Task<LogTransacao> GetByIdAsync(string id);
        Task<LogTransacao> CreateAsync(LogTransacao log);
    }
}
