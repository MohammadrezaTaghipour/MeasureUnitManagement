using System;

namespace MeasureUnitManagement.Domain.Exceptions
{
    public class ArgumentsForModifyingBasicMeasureUnitCannotBeNull : Exception
    {
        public ArgumentsForModifyingBasicMeasureUnitCannotBeNull()
        {

        }

        public ArgumentsForModifyingBasicMeasureUnitCannotBeNull(string message)
            : base(message)
        {

        }
    }
    
}
