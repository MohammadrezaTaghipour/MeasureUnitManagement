using MeasureUnitManagement.Domain.MeasureDimensions;
using MeasureUnitManagement.Infrastructure.Core;

namespace MeasureUnitManagement.Domain.Services
{
    public interface IMeasureDimensionService : IDomainService
    {
        double MeasureToBasicUnit(MeasureUnit unit, double value);
        double MeasureFromBasicUnit(MeasureUnit unit, double value);
    }
}
