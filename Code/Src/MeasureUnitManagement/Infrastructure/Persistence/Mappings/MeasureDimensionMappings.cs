using MeasureUnitManagement.Domain.MeasureDimensions;
using MeasureUnitManagement.Domain.MeasureDimensions.Entities;
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
                    cm.MapField("_coefficientUnits").SetElementName("CoefficientUnits");
                    cm.MapField("_formulatedUnits").SetElementName("FormulatedUnits");
                });
            }
            
            if (!BsonClassMap.IsClassMapRegistered(typeof(MeasureUnit)))
            {
                BsonClassMap.RegisterClassMap<MeasureUnit>(cm =>
                {
                    cm.AutoMap();
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(BasicMeasureUnit)))
            {
                BsonClassMap.RegisterClassMap<BasicMeasureUnit>(cm =>
                {
                    cm.AutoMap();
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(CoefficientMeasureUnit)))
            {
                BsonClassMap.RegisterClassMap<CoefficientMeasureUnit>(cm =>
                {
                    cm.AutoMap();
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(FormulatedMeasureUnit)))
            {
                BsonClassMap.RegisterClassMap<FormulatedMeasureUnit>(cm =>
                {
                    cm.AutoMap();
                });
            }


        }
    }
}
