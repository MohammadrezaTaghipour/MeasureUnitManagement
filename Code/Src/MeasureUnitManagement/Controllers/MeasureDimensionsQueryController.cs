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
        public Task<IEnumerable<MeasureDimensionResponse>> Get([FromQuery]int page,
            [FromQuery] int pageSize)
        {
            var query = new GetAllMeasureDimension() { Page = page, PageSize = pageSize };
            return _mediator.Send(query);
        }
    }
}
