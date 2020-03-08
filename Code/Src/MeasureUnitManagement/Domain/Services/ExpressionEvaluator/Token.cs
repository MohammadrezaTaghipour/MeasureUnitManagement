using System;

namespace MeasureUnitManagement.Domain.Services.ExpressionEvaluator
{
    public enum Token
    {
        EOF,
        Add,
        Subtract,
        Multiply,
        Divide,
        OpenParens,
        CloseParens,
        Comma,
        Identifier,
        Number,
    }
}
