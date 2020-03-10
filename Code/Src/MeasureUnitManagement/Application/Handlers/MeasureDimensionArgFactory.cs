using MeasureUnitManagement.Application.Commands;
using MeasureUnitManagement.Domain.MeasureDimensions;
using MeasureUnitManagement.Domain.MeasureDimensions.Args;
using MeasureUnitManagement.Domain.MeasureDimensions.ValueObjects;

namespace MeasureUnitManagement.Application.Handlers
{
    public class MeasureDimensionArgFactory : IMeasureDimensionArgFactory
    {
        public BasicMeasureUnitArg MapToArg(AddBasicMeasureUnit request)
        {
            return new BasicMeasureUnitArg
            {
                Id = new Symbol(request.SymbolId),
                Title = request.Title,
                TitleSlug = request.TitleSlug
            };
        }

        public BasicMeasureUnitArg MapToArg(ModifyBasicMeasureUnit request)
        {
            return new BasicMeasureUnitArg
            {
                Id = new Symbol(request.SymbolId),
                Title = request.Title,
                TitleSlug = request.TitleSlug
            };
        }

        public CoefficientMeasureUnitArg MapToArg(AddCoefficientMeasureUnit request)
        {
            return new CoefficientMeasureUnitArg
            {
                Id = new Symbol(request.SymbolId),
                Title = request.Title,
                TitleSlug = request.TitleSlug,
                RatioFromBasicMeasureUnit = new RatioFromBasicMeasureUnit(request.RatioFromBasicMeasureUnit)
            };
        }

        public CoefficientMeasureUnitArg MapToArg(ModifyCoefficientMeasureUnit request)
        {
            return new CoefficientMeasureUnitArg
            {
                Id = new Symbol(request.SymbolId),
                Title = request.Title,
                TitleSlug = request.TitleSlug,
                RatioFromBasicMeasureUnit = new RatioFromBasicMeasureUnit(request.RatioFromBasicMeasureUnit)
            };
        }

        public FormulatedMeasureUnitArg MapToArg(AddFormulatedMeasureUnit request)
        {
            return new FormulatedMeasureUnitArg
            {
                Id = new Symbol(request.SymbolId),
                Title = request.Title,
                TitleSlug = request.TitleSlug,
                ConvertFormulaFromBasicUnit = new ConvertFormulaArg { Formula = request.ConvertFormulaFromBasicUnit },
                ConvertFormulaToBasicUnit = new ConvertFormulaArg { Formula = request.ConvertFormulaToBasicUnit }
            };
        }

        public FormulatedMeasureUnitArg MapToArg(ModifyFormulatedMeasureUnit request)
        {
            return new FormulatedMeasureUnitArg
            {
                Id = new Symbol(request.SymbolId),
                Title = request.Title,
                TitleSlug = request.TitleSlug,
                ConvertFormulaFromBasicUnit = new ConvertFormulaArg { Formula = request.ConvertFormulaFromBasicUnit },
                ConvertFormulaToBasicUnit = new ConvertFormulaArg { Formula = request.ConvertFormulaToBasicUnit }
            };
        }
    }
}