using MessageConsumer.Application.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MessageConsumer.Infra
{
    public class TransacaoMongoRepository : ITransacaoMongoRepository
    {
        private readonly IMongoCollection<Invitation> _collection;
        private readonly DbConfiguration _settings;

        public TransacaoMongoRepository(IOptions<DbConfiguration> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _collection = database.GetCollection<Invitation>(_settings.CollectionName);
        }

        public async Task<Invitation> CreateAsync(Invitation log)
        {
            try
            {
                await _collection.InsertOneAsync(log);
                return log;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public Task<List<Invitation>> GetAllAsync()
        {
            return _collection.Find(c => true).ToListAsync();
        }

        public Task<Invitation> GetByIdAsync(string id)
        {
            Expression<Func<Invitation, bool>> filter = ObterFilter(id);

            var servidor = _collection.Find(filter).FirstOrDefaultAsync();

            return servidor;
        }

        private static Expression<Func<Invitation, bool>> ObterFilter(string id)
        {
            return x => x.Id.Equals(ObjectId.Parse(id));
        }
    }
}
