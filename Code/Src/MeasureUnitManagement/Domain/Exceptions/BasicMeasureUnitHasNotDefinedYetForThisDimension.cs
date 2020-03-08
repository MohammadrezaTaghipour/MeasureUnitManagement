using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeasureUnitManagement.Domain.Exceptions
{
    public class BasicMeasureUnitHasNotDefinedYetForThisDimension :
        Exception
    {
        public BasicMeasureUnitHasNotDefinedYetForThisDimension()
        { }

        public BasicMeasureUnitHasNotDefinedYetForThisDimension(string message)
            : base(message)
        { }
    }
}
