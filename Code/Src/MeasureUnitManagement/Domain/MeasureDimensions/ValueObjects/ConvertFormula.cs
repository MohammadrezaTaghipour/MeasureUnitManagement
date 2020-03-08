using MeasureUnitManagement.Domain.Services.ExpressionEvaluator;
using MeasureUnitManagement.Infrastructure.Core;
using System.Collections.Generic;

namespace MeasureUnitManagement.Domain.MeasureDimensions.ValueObjects
{
    public class ConvertFormula : ValueObject<ConvertFormula>
    {
        public ConvertFormula(string formula)
        {
            GuardAgainstFormulaFormat(formula);
            this.Formula = formula;
        }

        public string Formula { get; private set; }

        public double MeasureBy(double value,
            IFormulaExpressionEvluator formulaExpressionEvluator)
        {
            string exp = Formula.Replace("a", value.ToString());
            return formulaExpressionEvluator.Evaluate(exp);
        }

        private void GuardAgainstFormulaFormat(string formula)
        { }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<string> { Formula };
        }
    }
}
