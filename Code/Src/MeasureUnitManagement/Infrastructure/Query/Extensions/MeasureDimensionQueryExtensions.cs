using MeasureUnitManagement.Domain.MeasureDimensions;
using MeasureUnitManagement.Infrastructure.Query.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeasureUnitManagement.Infrastructure.Query.Extensions
{
    public static class MeasureDimensionQueryExtensions
    {
        public static MeasureDimensionResponse MapToQueryResponse(this MeasureDimension measureDimension)
        {
            return new MeasureDimensionResponse { };
        }

        public static IEnumerable<MeasureDimensionResponse> MapToQueryResponse(
            this IEnumerable<MeasureDimension> measureDimensions)
        {
            foreach (var dimension in measureDimensions)
            {
                yield return MapToQueryResponse(dimension);
            }
        }
    }
}
