using System;

namespace MeasureUnitManagement.Domain.Exceptions
{
    public class ArgumentsForModifingFormulatedMeasureUnitCannotBeNull :
        Exception
    {
        public ArgumentsForModifingFormulatedMeasureUnitCannotBeNull()
        {

        }

        public ArgumentsForModifingFormulatedMeasureUnitCannotBeNull(string message)
            : base(message)
        {

        }
    }
}
