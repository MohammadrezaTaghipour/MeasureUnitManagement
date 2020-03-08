using System;

namespace MeasureUnitManagement.Domain.Services.ExpressionEvaluator
{
    internal class NodeNumber : Node
    {
        public NodeNumber(double number)
        {
            _number = number;
        }

        double _number;             // The number

        public override double Eval()
        {
            // Just return it.  Too easy.
            return _number;
        }
    }
}
