using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeasureUnitManagement.Domain.Exceptions
{
    public class BasicMeasureUnitHasBennDefinedBeforeForThisDimension : Exception
    {
        public BasicMeasureUnitHasBennDefinedBeforeForThisDimension()
        { }

        public BasicMeasureUnitHasBennDefinedBeforeForThisDimension(string message)
            : base(message)
        { }
    }
}
