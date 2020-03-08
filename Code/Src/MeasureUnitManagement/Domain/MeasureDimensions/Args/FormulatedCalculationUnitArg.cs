namespace MeasureUnitManagement.Domain.MeasureDimensions.Args
{
    public class FormulatedMeasureUnitArg
    {
        public Symbol Id { get; set; }
        public string Title { get; set; }
        public string TitleSlug { get; set; }
        public ConvertFormulaArg ConvertFormulaFromBasicUnit { get; set; }
        public ConvertFormulaArg ConvertFormulaToBasicUnit { get; set; }
    }

    public class ConvertFormulaArg
    {
        public string Formula { get; set; }
    }

}
