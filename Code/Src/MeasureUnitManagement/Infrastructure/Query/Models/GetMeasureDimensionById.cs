﻿using MediatR;

namespace MeasureUnitManagement.Infrastructure.Query.Models
{
    public class GetMeasureDimensionById : IRequest<MeasureDimensionResponse>
    {
        public long Id { get; set; }
    }
}
