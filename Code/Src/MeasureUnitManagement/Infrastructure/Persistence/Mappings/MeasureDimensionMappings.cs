using MeasureUnitManagement.Domain.MeasureDimensions;
using MeasureUnitManagement.Infrastructure.Core;
using MongoDB.Bson.Serialization;

namespace MeasureUnitManagement.Infrastructure.Persistence.Mappings
{
    public class MeasureDimensionMappings
    {
        public void Register()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Entity<long>)))
            {
                BsonClassMap.RegisterClassMap<Entity<long>>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdMember(c => c.Id);
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(MeasureDimension)))
            {
                BsonClassMap.RegisterClassMap<MeasureDimension>(cm =>
                {
                    cm.AutoMap();
                });
            }
        }
    }
}
