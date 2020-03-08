using MeasureUnitManagement.Infrastructure.Core;
using System.Collections.Generic;

namespace MeasureUnitManagement.Domain.MeasureDimensions
{
    public class Symbol : ValueObject<Symbol>
    {
        public string Id { get; private set; }

        public Symbol(string id)
        {
            this.Id = id;
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<string> { Id };
        }
    }
}
