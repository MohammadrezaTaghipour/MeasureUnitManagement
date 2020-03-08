using System;

namespace MeasureUnitManagement.Domain.Exceptions
{
    public class InvalidMeasureUnit : Exception
    {
        public InvalidMeasureUnit()
        { }

        public InvalidMeasureUnit(string message) : base(message)
        { }
    }
}
