using System;

namespace MeasureUnitManagement.Domain.Exceptions
{
    public class ArgumentsForDefiningBasicMeasureUnitCannotBeNull : Exception
    {
        public ArgumentsForDefiningBasicMeasureUnitCannotBeNull()
        {

        }

        public ArgumentsForDefiningBasicMeasureUnitCannotBeNull(string message)
            : base(message)
        {

        }
    }
}
