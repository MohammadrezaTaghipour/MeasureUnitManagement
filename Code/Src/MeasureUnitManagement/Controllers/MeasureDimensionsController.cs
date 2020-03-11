﻿using System.Threading.Tasks;
using MeasureUnitManagement.Application.Commands;
using MeasureUnitManagement.Infrastructure.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MeasureUnitManagement.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public partial class MeasureDimensionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MeasureDimensionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public Task<long> Post(CreateMeasureDimensionCommand command)
        {
            return _mediator.Send(command);
        }

        [HttpPost]
        [Route("{id}/basicUnit")]
        public Task<long> Post(long id, AddBasicMeasureUnit command)
        {
            command.MeasureDimensionId = id;
            return _mediator.Send(command);
        }

        [HttpPut]
        [Route("{id}/basicUnit")]
        public Task<long> Put(long id, ModifyBasicMeasureUnit command)
        {
            command.MeasureDimensionId = id;
            return _mediator.Send(command);
        }

        [HttpPost]
        [Route("{id}/coefficientUnit")]
        public Task<long> Post(long id, AddCoefficientMeasureUnit command)
        {
            command.MeasureDimensionId = id;
            return _mediator.Send(command);
        }

        [HttpPut]
        [Route("{id}/coefficientUnit")]
        public Task<long> Put(long id, ModifyCoefficientMeasureUnit command)
        {
            command.MeasureDimensionId = id;
            return _mediator.Send(command);
        }

        [HttpPost]
        [Route("{id}/formulatedUnit")]
        public Task<long> Post(long id, AddFormulatedMeasureUnit command)
        {
            command.MeasureDimensionId = id;
            return _mediator.Send(command);
        }

        [HttpPut]
        [Route("{id}/formulatedUnit")]
        public Task<long> Put(long id, ModifyFormulatedMeasureUnit command)
        {
            command.MeasureDimensionId = id;
            return _mediator.Send(command);
        }

        [HttpGet]
        [Route("{id}/from/{fromUnitSymbol}/to/{toUnitSymbol}/{value}")]
        public Task<double> Get(long id, string fromUnitSymbol, string toUnitSymbol, double value)
        {
            var command = new MeasureUnitCommand
            {
                DimensionId = id,
                Value = value,
                FromUnitSymbol = fromUnitSymbol,
                ToUnitSymbol = toUnitSymbol
            };
            return _mediator.Send(command);
        }

    }
}
