using System;

namespace MeasureUnitManagement.Domain.Exceptions
{
    public class ArgumentsForDefiningFormulatedMeasureUnitCannotBeNull :
        Exception
    {
        public ArgumentsForDefiningFormulatedMeasureUnitCannotBeNull()
        {

        }

        public ArgumentsForDefiningFormulatedMeasureUnitCannotBeNull(string message)
            : base(message)
        {

        }
    }
}
