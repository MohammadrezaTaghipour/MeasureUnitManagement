using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MeasureUnitManagement.Infrastructure.DataAccess
{
    public class SequenceId
    {
        [BsonId] public ObjectId Id { get; set; }
        public string AggregateName { get; set; }
        public long SeqId { get; set; }
    }
}
