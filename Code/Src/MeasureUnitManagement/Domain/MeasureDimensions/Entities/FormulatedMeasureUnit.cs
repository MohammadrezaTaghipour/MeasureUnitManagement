using MeasureUnitManagement.Domain.MeasureDimensions.ValueObjects;
using MeasureUnitManagement.Domain.Services.ExpressionEvaluator;

namespace MeasureUnitManagement.Domain.MeasureDimensions.Entities
{
    public class FormulatedMeasureUnit : MeasureUnit
    {
        protected FormulatedMeasureUnit(Symbol id, string title, string titleSlug,
            ConvertFormula convertFormulaFromBasicUnit,
            ConvertFormula convertFormulaToBasicUnit)
            : base(id, title, titleSlug)
        {
            this.ConvertFormulaFromBasicUnit = convertFormulaFromBasicUnit;
            this.ConvertFormulaToBasicUnit = convertFormulaToBasicUnit;
        }

        public ConvertFormula ConvertFormulaFromBasicUnit { get; private set; }
        public ConvertFormula ConvertFormulaToBasicUnit { get; private set; }

        public static FormulatedMeasureUnit Create(Symbol id,
            string title, string titleSlug, ConvertFormula convertFormulaFromBasicUnit,
            ConvertFormula convertFormulaToBasicUnit)
        {
            var unit = new FormulatedMeasureUnit(id, title, titleSlug,
                convertFormulaFromBasicUnit, convertFormulaToBasicUnit);
            return unit;
        }

        public void Modify(string title, string titleSlug,
            ConvertFormula convertFormulaFromBasicUnit,
            ConvertFormula convertFormulaToBasicUnit)
        {
            base.Modify(title, titleSlug);
            this.ConvertFormulaFromBasicUnit = convertFormulaFromBasicUnit;
            this.ConvertFormulaToBasicUnit = convertFormulaToBasicUnit;
        }

        public double MesaureToBasicUnit(double value, IFormulaExpressionEvaluator evaluator)
        {
            return this.ConvertFormulaToBasicUnit.MeasureBy(value,
                evaluator);
        }

        public double MesaureFromBasicUnit(double value, IFormulaExpressionEvaluator evaluator)
        {
            return this.ConvertFormulaFromBasicUnit.MeasureBy(value,
                evaluator);
        }

    }
}
