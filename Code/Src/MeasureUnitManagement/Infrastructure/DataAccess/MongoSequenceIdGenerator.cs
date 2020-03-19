using MongoDB.Driver;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace MeasureUnitManagement.Infrastructure.DataAccess
{
    public class MongoSequenceIdGenerator : ISequenceIdGenerator
    {
        private readonly IMongoDatabase _mongo;
        private static readonly ConcurrentDictionary<string, object> _locks = new ConcurrentDictionary<string, object>();

        public MongoSequenceIdGenerator(IMongoDatabase mongo)
        {
            this._mongo = mongo;
        }

        public Task<long> GetNextSequence<TAggregate>()
        {
            var aggName = GetStreamName<TAggregate>();
            var lockObj = _locks.GetOrAdd(aggName, (a) => new object());
            lock (lockObj)
            {
                var lastId = ReadLastSequence(aggName).Result;
                UpdateLastSequence(aggName, 1).Wait();
                return Task.FromResult(++lastId);
            }
        }

        private string GetStreamName<TAggregate>()
        {
            return $"{typeof(TAggregate).Name}-SequenceId";
        }

        private async Task<long> ReadLastSequence(string aggregateName)
        {
            var result = await GetCollection()
                .Find(a => a.AggregateName == aggregateName)
                .FirstOrDefaultAsync();
            return result?.SeqId ?? 0;
        }

        public Task UpdateLastSequence(string aggregateName, int incrementSize)
        {
            var filter = new FilterDefinitionBuilder<SequenceId>().Eq(a => a.AggregateName, aggregateName);
            var command = new UpdateDefinitionBuilder<SequenceId>().Inc(n => n.SeqId, incrementSize);
            var options = new FindOneAndUpdateOptions<SequenceId>()
            {
                IsUpsert = true,
            };
            return GetCollection().FindOneAndUpdateAsync(filter, command, options);
        }

        private IMongoCollection<SequenceId> GetCollection()
        {
            return _mongo.GetCollection<SequenceId>(nameof(SequenceId));
        }
    }
}
