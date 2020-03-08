using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeasureUnitManagement.Domain.MeasureDimensions.Args
{
    public class BasicMeasureUnitArg
    {
        public Symbol Id { get; set; }
        public string Title { get; set; }
        public string TitleSlug { get; set; }
    }
}
