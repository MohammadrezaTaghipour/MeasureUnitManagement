using System;

namespace MeasureUnitManagement.Domain.Exceptions
{
    public class ParenthesisAreNotBalanced : Exception
    {
        public ParenthesisAreNotBalanced()
        { }

        public ParenthesisAreNotBalanced(string message) : base(message)
        { }
    }
}
