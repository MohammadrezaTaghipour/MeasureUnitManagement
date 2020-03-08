using MeasureUnitManagement.Infrastructure.Core;
using System.Collections.Generic;

namespace MeasureUnitManagement.Domain.MeasureDimensions.ValueObjects
{
    public class RatioFromBasicMeasureUnit : ValueObject<RatioFromBasicMeasureUnit>
    {
        public double Value { get; private set; }

        public RatioFromBasicMeasureUnit(double value)
        {
            this.Value = value;
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<object> { Value };
        }
    }
}
