using MeasureUnitManagement.Infrastructure.Core;
using System;

namespace MeasureUnitManagement.Domain.Services.ExpressionEvaluator
{
    public interface IFormulaExpressionEvluator : IDomainService
    {
        double Evaluate(string expression);
    }
}
