using MeasureUnitManagement.Application.Commands;
using MeasureUnitManagement.Domain.MeasureDimensions.Args;

namespace MeasureUnitManagement.Application.Handlers
{
    public interface IMeasureDimensionArgFactory
    {
        BasicMeasureUnitArg MapToArg(AddBasicMeasureUnit request);
        BasicMeasureUnitArg MapToArg(ModifyBasicMeasureUnit request);
        CoefficientMeasureUnitArg MapToArg(AddCoefficientMeasureUnit request);
        CoefficientMeasureUnitArg MapToArg(ModifyCoefficientMeasureUnit request);
        FormulatedMeasureUnitArg MapToArg(AddFormulatedMeasureUnit request);
        FormulatedMeasureUnitArg MapToArg(ModifyFormulatedMeasureUnit request);
    }
}
