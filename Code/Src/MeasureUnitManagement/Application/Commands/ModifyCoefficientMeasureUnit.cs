using MediatR;

namespace MeasureUnitManagement.Application.Commands
{
    public class ModifyCoefficientMeasureUnit : IRequest<long>
    {
        public long MeasureDimensionId { get; set; }
        public string SymbolId { get; set; }
        public string Title { get; set; }
        public string TitleSlug { get; set; }
        public double RatioFromBasicMeasureUnit { get; set; }
    }
}