using MeasureUnitManagement.Infrastructure.Query.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MeasureUnitManagement.Infrastructure.Query.Handlers
{
    public class MeasureDimensionQueryHandler :
        IRequestHandler<GetMeasureDimensionById, MeasureDimensionResponse>
    {
        public Task<MeasureDimensionResponse> Handle(GetMeasureDimensionById request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
