using System;

namespace MeasureUnitManagement.Domain.Exceptions
{
    public class ArgumentsForDefiningCoefficientMeasureUnitCannotBeNull : Exception
    {
        public ArgumentsForDefiningCoefficientMeasureUnitCannotBeNull()
        {

        }

        public ArgumentsForDefiningCoefficientMeasureUnitCannotBeNull(string message)
            : base(message)
        {

        }
    }
    
}
