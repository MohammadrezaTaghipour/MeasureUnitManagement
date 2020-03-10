﻿using MeasureUnitManagement.Application.Commands;
using MeasureUnitManagement.Domain.MeasureDimensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MeasureUnitManagement.Application.Handlers
{
    public class MeasureDimensionCommandHandler :
        IRequestHandler<CreateMeasureDimensionCommand, long>,
        IRequestHandler<AddBasicMeasureUnit, long>,
        IRequestHandler<ModifyBasicMeasureUnit, long>,
        IRequestHandler<AddCoefficientMeasureUnit, long>,
        IRequestHandler<ModifyCoefficientMeasureUnit, long>,
        IRequestHandler<AddFormulatedMeasureUnit, long>,
        IRequestHandler<ModifyFormulatedMeasureUnit, long>
    {
        private readonly IMeasureDimensionRepository _measureDimensionRepository;
        private readonly IMeasureDimensionArgFactory _measureDimensionArgFactory;

        public MeasureDimensionCommandHandler(IMeasureDimensionRepository measureDimensionRepository,
            IMeasureDimensionArgFactory measureDimensionArgFactory)
        {
            _measureDimensionRepository = measureDimensionRepository;
            _measureDimensionArgFactory = measureDimensionArgFactory;
        }

        public async Task<long> Handle(CreateMeasureDimensionCommand request, CancellationToken cancellationToken)
        {
            var id = await _measureDimensionRepository.GetNextId();
            var dimension = MeasureDimension.Create(id, request.Title);
            await _measureDimensionRepository.Add(dimension, cancellationToken);
            return id;
        }

        public async Task<long> Handle(AddBasicMeasureUnit request, CancellationToken cancellationToken)
        {
            var dimension = await _measureDimensionRepository.GetById(request.MeasureDimensionId);
            dimension.DefineBasicMeasureUnit(_measureDimensionArgFactory.MapToArg(request));
            await _measureDimensionRepository.Add(dimension, cancellationToken);
            return dimension.Id;
        }

        public async Task<long> Handle(ModifyBasicMeasureUnit request, CancellationToken cancellationToken)
        {
            var dimension = await _measureDimensionRepository.GetById(request.MeasureDimensionId);
            dimension.ModifyBasicMeasureUnit(_measureDimensionArgFactory.MapToArg(request));
            await _measureDimensionRepository.Add(dimension, cancellationToken);
            return dimension.Id;
        }

        public async Task<long> Handle(AddCoefficientMeasureUnit request, CancellationToken cancellationToken)
        {
            var dimension = await _measureDimensionRepository.GetById(request.MeasureDimensionId);
            dimension.DefineCoefficientUnit(_measureDimensionArgFactory.MapToArg(request));
            await _measureDimensionRepository.Add(dimension, cancellationToken);
            return dimension.Id;
        }

        public async Task<long> Handle(ModifyCoefficientMeasureUnit request, CancellationToken cancellationToken)
        {
            var dimension = await _measureDimensionRepository.GetById(request.MeasureDimensionId);
            dimension.ModifyCoefficientUnit(_measureDimensionArgFactory.MapToArg(request));
            await _measureDimensionRepository.Add(dimension, cancellationToken);
            return dimension.Id;
        }

        public async Task<long> Handle(AddFormulatedMeasureUnit request, CancellationToken cancellationToken)
        {
            var dimension = await _measureDimensionRepository.GetById(request.MeasureDimensionId);
            dimension.DefineFormulatedUnit(_measureDimensionArgFactory.MapToArg(request));
            await _measureDimensionRepository.Add(dimension, cancellationToken);
            return dimension.Id;
        }

        public async Task<long> Handle(ModifyFormulatedMeasureUnit request, CancellationToken cancellationToken)
        {
            var dimension = await _measureDimensionRepository.GetById(request.MeasureDimensionId);
            dimension.ModifyFormulatedUnit(_measureDimensionArgFactory.MapToArg(request));
            await _measureDimensionRepository.Add(dimension, cancellationToken);
            return dimension.Id;
        }
    }
}
