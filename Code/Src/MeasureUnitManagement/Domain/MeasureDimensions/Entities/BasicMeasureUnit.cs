using MeasureUnitManagement.Domain.Services.ExpressionEvaluator;

namespace MeasureUnitManagement.Domain.MeasureDimensions.Entities
{
    public class BasicMeasureUnit : MeasureUnit
    {
        protected BasicMeasureUnit(Symbol id, string title, string titleSlug)
            : base(id, title, titleSlug)
        { }

        public static BasicMeasureUnit Create(Symbol id, string title, string titleSlug)
        {
            var basicUnit = new BasicMeasureUnit(id, title, titleSlug);
            return basicUnit;
        }

        public override double MeasureFromBasicUnit(double value,
            IFormulaExpressionEvaluator expressionEvaluator = null)
        {
            return value * 1;
        }

        public override double MeasureToBasicUnit(double value,
            IFormulaExpressionEvaluator expressionEvaluator = null)
        {
            return value * 1;
        }

        public new void Modify(string title, string titleSlug)
        {
            base.Modify(title, titleSlug);
        }
    }
}
