namespace MeasureUnitManagement.Infrastructure.Query.Models
{
    public class FormulatedMeasureUnitResponse
    {
        public string Symbol { get; set; }
        public string Title { get; set; }
        public string TitleSlug { get; set; }
        public string ConvertFormulaFromBasicUnit { get; set; }
        public string ConvertFormulaToBasicUnit { get; set; }
    }
}
