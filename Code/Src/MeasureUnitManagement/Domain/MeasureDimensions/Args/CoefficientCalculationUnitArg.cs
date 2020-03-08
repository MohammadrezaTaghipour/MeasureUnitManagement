using MeasureUnitManagement.Domain.MeasureDimensions.ValueObjects;

namespace MeasureUnitManagement.Domain.MeasureDimensions.Args
{
    public class CoefficientMeasureUnitArg
    {
        public Symbol Id { get; set; }
        public string Title { get; set; }
        public string TitleSlug { get; set; }
        public RatioFromBasicMeasureUnit RatioFromBasicMeasureUnit { get; set; }
    }
}
