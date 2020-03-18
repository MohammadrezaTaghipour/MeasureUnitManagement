using MeasureUnitManagement.Domain.MeasureDimensions;
using MeasureUnitManagement.Infrastructure.Query.Extensions;
using MeasureUnitManagement.Infrastructure.Query.Models;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MeasureUnitManagement.Infrastructure.Query.Handlers
{
    public class MeasureDimensionQueryHandler :
        IRequestHandler<GetMeasureDimensionById, MeasureDimensionResponse>,
        IRequestHandler<GetAllMeasureDimension, IEnumerable<MeasureDimensionResponse>>
    {
        private readonly IMongoDatabase _mongo;
        private const string CollectionName = nameof(MeasureDimension);

        public MeasureDimensionQueryHandler(IMongoDatabase mongo)
        {
            this._mongo = mongo;
        }

        public async Task<MeasureDimensionResponse> Handle(GetMeasureDimensionById request,
            CancellationToken cancellationToken)
        {
            var collection = _mongo.GetCollection<MeasureDimension>(CollectionName);
            var dimension = await collection.Find(d => d.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            return dimension.MapToQueryResponse();
        }

        public async Task<IEnumerable<MeasureDimensionResponse>> Handle(GetAllMeasureDimension request,
            CancellationToken cancellationToken)
        {
            var collection = _mongo.GetCollection<MeasureDimension>(CollectionName);
            var dimensions = await collection.Find(new FilterDefinitionBuilder<MeasureDimension>().Empty)
                .Skip((request.Page - 1) * request.PageSize)
                .Limit(request.PageSize)
                .ToListAsync(cancellationToken);
            return dimensions.MapToQueryResponse();
        }
    }
}
