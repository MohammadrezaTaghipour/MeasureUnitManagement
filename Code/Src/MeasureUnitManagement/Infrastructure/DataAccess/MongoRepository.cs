using MeasureUnitManagement.Infrastructure.Core;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace MeasureUnitManagement.Infrastructure.DataAccess
{
    public class MongoRepository<TKey, TAggregate> : IDocumentBasedRepository<TKey, TAggregate>
        where TAggregate : AggregateRoot<TKey>
    {
        private readonly IMongoDatabase _mongo;
        private readonly ISequenceIdGenerator _idGenerator;

        public MongoRepository(IMongoDatabase mongo,
            ISequenceIdGenerator idGenerator)
        {
            this._mongo = mongo;
            this._idGenerator = idGenerator;
        }

        public Task Add(TAggregate aggregate, CancellationToken cancellationToken)
        {
            var option = new InsertOneOptions { BypassDocumentValidation = true };
            return this.GetCollection().InsertOneAsync(aggregate, option, cancellationToken);
        }

        public Task UpSert(TAggregate aggregate, CancellationToken cancellationToken)
        {
            var filter = new FilterDefinitionBuilder<TAggregate>().Eq(a => a.Id, aggregate.Id);
            var options = new ReplaceOptions()
            {
                BypassDocumentValidation = true,
                IsUpsert = true,
            };
            return GetCollection().ReplaceOneAsync(filter, aggregate, options, cancellationToken);
        }

        public Task<TAggregate> GetById(TKey id)
        {
            var filter = new FilterDefinitionBuilder<TAggregate>().Eq(f => f.Id, id);
            return this.GetCollection().Find(filter).FirstOrDefaultAsync();
        }

        public Task<long> GetNextId()
        {
            return _idGenerator.GetNextSequence<TAggregate>();
        }

        private IMongoCollection<TAggregate> GetCollection()
        {
            return _mongo.GetCollection<TAggregate>(typeof(TAggregate).Name);
        }
    }
}
