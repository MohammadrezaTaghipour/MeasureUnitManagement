using MeasureUnitManagement.Infrastructure.Core;
using System;

namespace MeasureUnitManagement.Domain.Services.ExpressionEvaluator
{
    public interface IFormulaExpressionEvaluator : IDomainService
    {
        double Evaluate(string expression);
    }
}
 