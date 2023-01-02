using MessageConsumer.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessageConsumer.Infra
{
    public interface ITransacaoMongoRepository
    {
        Task<List<Invitation>> GetAllAsync();
        Task<Invitation> GetByIdAsync(string id);
        Task<Invitation> CreateAsync(Invitation invitation);
    }
}
