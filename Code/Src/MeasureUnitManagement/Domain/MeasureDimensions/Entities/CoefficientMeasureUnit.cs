using MeasureUnitManagement.Domain.MeasureDimensions.ValueObjects;
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

        public double MesaureToBasicUnit(double value)
        {
            return value * this.RatioFromBasicMeasureUnit.Value;
        }

        public double MesaureFromBasicUnit(double value)
        {
            return value / this.RatioFromBasicMeasureUnit.Value;
        }
    }
}
