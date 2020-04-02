using MeasureUnitManagement.Domain.MeasureDimensions.ValueObjects;
using MeasureUnitManagement.Domain.Services.ExpressionEvaluator;
using System;

namespace MeasureUnitManagement.Domain.MeasureDimensions.Entities
{
    public class CoefficientMeasureUnit : MeasureUnit
    {
        protected CoefficientMeasureUnit(Symbol id, string title,
            string titleSlug, RatioFromBasicMeasureUnit ratioFromBasicMeasureUnit)
            : base(id, title, titleSlug)
        {
            this.RatioFromBasicMeasureUnit = ratioFromBasicMeasureUnit;
        }

        public RatioFromBasicMeasureUnit RatioFromBasicMeasureUnit { get; private set; }

        public static CoefficientMeasureUnit Create(Symbol id, string title,
            string titleSlug, RatioFromBasicMeasureUnit ratioFromBasicMeasureUnit)
        {
            var coefficientUnit = new CoefficientMeasureUnit(id, title,
                titleSlug, ratioFromBasicMeasureUnit);
            return coefficientUnit;
        }

        public void Modify(string title,
            string titleSlug, RatioFromBasicMeasureUnit ratioFromBasicMeasureUnit)
        {
            base.Modify(title, titleSlug);
            this.RatioFromBasicMeasureUnit = ratioFromBasicMeasureUnit;
        }

        public override double MeasureToBasicUnit(double value,
            IFormulaExpressionEvaluator expressionEvaluator = null)
        {
            return value * this.RatioFromBasicMeasureUnit.Value;
        }

        public override double MeasureFromBasicUnit(double value,
            IFormulaExpressionEvaluator expressionEvaluator = null)
        {
            return value / this.RatioFromBasicMeasureUnit.Value;
        }
    }
}
