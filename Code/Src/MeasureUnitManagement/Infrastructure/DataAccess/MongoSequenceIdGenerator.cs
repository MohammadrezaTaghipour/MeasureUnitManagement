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
                var nextId = ReadLastReservedNumber(aggName).Result;
                return Task.FromResult(nextId);
            }
        }

        private string GetStreamName<TAggregate>()
        {
            return $"{typeof(TAggregate).Name}-SequenceId";
        }

        private async Task<long> ReadLastReservedNumber(string aggregateName)
        {
            var result = await GetCollection()
                .Find(a => a.AggregateName == aggregateName)
                .FirstOrDefaultAsync();
            return result?.SeqId ?? 0;
        }

        private IMongoCollection<SequenceId> GetCollection()
        {
            return _mongo.GetCollection<SequenceId>(nameof(SequenceId));
        }
    }
}
