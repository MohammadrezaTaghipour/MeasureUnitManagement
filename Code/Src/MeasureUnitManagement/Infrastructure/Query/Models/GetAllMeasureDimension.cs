using MediatR;
using System.Collections.Generic;

namespace MeasureUnitManagement.Infrastructure.Query.Models
{
    public class GetAllMeasureDimension : IRequest<IEnumerable<MeasureDimensionResponse>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
