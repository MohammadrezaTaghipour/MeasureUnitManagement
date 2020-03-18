using MeasureUnitManagement.Application.Commands;
using MeasureUnitManagement.Infrastructure.Query.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeasureUnitManagement.Controllers
{
    public partial class MeasureDimensionsController
    {

        [HttpGet]
        [Route("{id}")]
        public Task<MeasureDimensionResponse> Get(long id)
        {
            var query = new GetMeasureDimensionById { Id = id };
            return _mediator.Send(query);
        }

        [HttpGet]
        public Task<IEnumerable<MeasureDimensionResponse>> Get([FromQuery]int page, [FromQuery] int pageSize)
        {
            var query = new GetAllMeasureDimension() { Page = page, PageSize = pageSize };
            return _mediator.Send(query);
        }

        [HttpGet]
        [Route("{id}/from/{fromMeasureUnitSymbol}/to/{toMeasureUnitSymbol}/{value}")]
        public Task<double> Get(long id, string fromMeasureUnitSymbol, string toMeasureUnitSymbol,
            double value)
        {
            var command = new MeasureUnitCommand
            {
                DimensionId = id,
                Value = value,
                FromUnitSymbol = fromMeasureUnitSymbol,
                ToUnitSymbol = toMeasureUnitSymbol
            };
            return _mediator.Send(command);
        }
    }
}
