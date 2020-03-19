using System.Collections.Generic;

namespace MeasureUnitManagement.Infrastructure.Query.Models
{
    public class MeasureDimensionResponse
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public BasicMeasureUnitResponse BasicUnit { get; set; }
        public IEnumerable<CoefficientMeasureUnitResponse> CoefficientUnits { get; set; }
        public IEnumerable<FormulatedMeasureUnitResponse> FormulatedUnits { get; set; }
    }
}
