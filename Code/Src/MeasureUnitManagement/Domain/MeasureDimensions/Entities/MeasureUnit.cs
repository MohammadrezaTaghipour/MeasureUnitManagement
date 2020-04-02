using MeasureUnitManagement.Domain.Services.ExpressionEvaluator;
using MeasureUnitManagement.Infrastructure.Core;

namespace MeasureUnitManagement.Domain.MeasureDimensions
{
    public abstract class MeasureUnit : Entity<Symbol>
    {
        protected MeasureUnit(Symbol id, string title, string titleSlug)
        {
            this.Id = id;
            this.Title = title;
            this.TitleSlug = titleSlug;
        }

        public string Title { get; private set; }
        public string TitleSlug { get; private set; }

        protected void Modify(string title, string titleSlug)
        {
            this.Title = title;
            this.TitleSlug = titleSlug;
        }

        public abstract double MeasureToBasicUnit(double value,
            IFormulaExpressionEvaluator expressionEvaluator = null);

        public abstract double MeasureFromBasicUnit(double value,
            IFormulaExpressionEvaluator expressionEvaluator = null);
    }
}
