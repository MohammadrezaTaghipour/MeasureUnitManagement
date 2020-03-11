using MeasureUnitManagement.Domain.Exceptions;
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
            IFormulaExpressionEvaluator formulaExpressionEvaluator)
        {
            string exp = Formula.Replace("a", value.ToString());
            return formulaExpressionEvaluator.Evaluate(exp);
        }

        private void GuardAgainstFormulaFormat(string formula)
        {
            Stack<char> lastOpen = new Stack<char>();
            foreach (var c in formula)
            {
                switch (c)
                {
                    case ')':
                        if (lastOpen.Count == 0 || lastOpen.Pop() != '(')
                            throw new ParenthesisAreNotBalanced(formula);
                        break;
                    case ']':
                        if (lastOpen.Count == 0 || lastOpen.Pop() != '[')
                            throw new ParenthesisAreNotBalanced(formula);
                        break;
                    case '}':
                        if (lastOpen.Count == 0 || lastOpen.Pop() != '{')
                            throw new ParenthesisAreNotBalanced(formula);
                        break;
                    case '(':
                        lastOpen.Push(c);
                        break;
                    case '[':
                        lastOpen.Push(c);
                        break;
                    case '{':
                        lastOpen.Push(c);
                        break;
                }
            }
            if (lastOpen.Count != 0)
                throw new ParenthesisAreNotBalanced(formula);
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<string> { Formula };
        }
    }
}
