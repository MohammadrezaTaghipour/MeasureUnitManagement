using MediatR;

namespace MeasureUnitManagement.Application.Commands
{
    public class AddFormulatedMeasureUnit : IRequest<long>
    {
        public long MeasureDimensionId { get; set; }
        public string SymbolId { get; set; }
        public string Title { get; set; }
        public string TitleSlug { get; set; }
        public string ConvertFormulaFromBasicUnit { get; set; }
        public string ConvertFormulaToBasicUnit { get; set; }
    }
}