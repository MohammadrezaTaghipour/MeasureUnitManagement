using MeasureUnitManagement.Application.Commands;
using MeasureUnitManagement.Domain.MeasureDimensions.Args;
using MeasureUnitManagement.Infrastructure.Core;

namespace MeasureUnitManagement.Application.Handlers
{
    public interface IMeasureDimensionArgFactory : IFactory
    {
        BasicMeasureUnitArg MapToArg(AddBasicMeasureUnit request);
        BasicMeasureUnitArg MapToArg(ModifyBasicMeasureUnit request);
        CoefficientMeasureUnitArg MapToArg(AddCoefficientMeasureUnit request);
        CoefficientMeasureUnitArg MapToArg(ModifyCoefficientMeasureUnit request);
        FormulatedMeasureUnitArg MapToArg(AddFormulatedMeasureUnit request);
        FormulatedMeasureUnitArg MapToArg(ModifyFormulatedMeasureUnit request);
    }
}
