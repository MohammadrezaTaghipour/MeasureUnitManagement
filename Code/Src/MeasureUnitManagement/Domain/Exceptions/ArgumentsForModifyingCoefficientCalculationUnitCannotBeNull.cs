using System;

namespace MeasureUnitManagement.Domain.Exceptions
{
    public class ArgumentsForModifyingCoefficientMeasureUnitCannotBeNull : Exception
    {
        public ArgumentsForModifyingCoefficientMeasureUnitCannotBeNull()
        {

        }

        public ArgumentsForModifyingCoefficientMeasureUnitCannotBeNull(string message)
            : base(message)
        {

        }
    }
    
}
