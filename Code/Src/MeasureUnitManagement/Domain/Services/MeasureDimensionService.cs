using MeasureUnitManagement.Domain.Exceptions;
using MeasureUnitManagement.Domain.MeasureDimensions;
using MeasureUnitManagement.Domain.MeasureDimensions.Entities;
using MeasureUnitManagement.Domain.Services.ExpressionEvaluator;

namespace MeasureUnitManagement.Domain.Services
{
    public class MeasureDimensionService : IMeasureDimensionService
    {
        public MeasureDimensionService(IFormulaExpressionEvaluator formulaExpressionEvaluator)
        {
            this._formulaExpressionEvaluator = formulaExpressionEvaluator;
        }

        private readonly IFormulaExpressionEvaluator _formulaExpressionEvaluator;

        public double MeasureFromBasicUnit(MeasureUnit unit, double value)
        {
            if (unit is BasicMeasureUnit basicUnit)
                return basicUnit.MeasureFromBasicUnit(value);

            if (unit is CoefficientMeasureUnit coeffientUnit)
                return coeffientUnit.MeasureFromBasicUnit(value);

            if (unit is FormulatedMeasureUnit formulatedUnit)
                return formulatedUnit.MeasureFromBasicUnit(value, _formulaExpressionEvaluator);

            throw new InvalidMeasureUnit($"type: {unit.GetType()}");
        }

        public double MeasureToBasicUnit(MeasureUnit unit, double value)
        {
            if (unit is BasicMeasureUnit basicUnit)
                return basicUnit.MeasureToBasicUnit(value);

            if (unit is CoefficientMeasureUnit coeffientUnit)
                return coeffientUnit.MeasureToBasicUnit(value);

            if (unit is FormulatedMeasureUnit formulatedUnit)
                return formulatedUnit.MeasureToBasicUnit(value, _formulaExpressionEvaluator);

            throw new InvalidMeasureUnit($"type: {unit.GetType()}");
        }
    }
}
