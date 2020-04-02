using MeasureUnitManagement.Domain.MeasureDimensions;
using MeasureUnitManagement.Domain.MeasureDimensions.Entities;
using MeasureUnitManagement.Infrastructure.Query.Models;
using System.Collections.Generic;
using System.Linq;

namespace MeasureUnitManagement.Infrastructure.Query.Extensions
{
    public static class MeasureDimensionQueryExtensions
    {
        public static MeasureDimensionResponse MapToQueryResponse(this MeasureDimension measureDimension)
        {
            var dimension = new MeasureDimensionResponse
            {
                Id = measureDimension.Id,
                Title = measureDimension.Title,
            };

            var basicUnit = measureDimension.MeasureUnits.OfType<BasicMeasureUnit>().FirstOrDefault();
            if (basicUnit != null)
            {
                dimension.BasicUnit = new BasicMeasureUnitResponse
                {
                    Symbol = basicUnit.Id.Id,
                    Title = basicUnit.Title,
                    TitleSlug = basicUnit.TitleSlug
                };
            }

            var coefficientUnits = measureDimension.MeasureUnits.OfType<CoefficientMeasureUnit>().ToList();
            if (coefficientUnits != null && coefficientUnits.Any())
            {
                dimension.CoefficientUnits = coefficientUnits.Select(c => new CoefficientMeasureUnitResponse
                {
                    Symbol = c.Id.Id,
                    Title = c.Title,
                    TitleSlug = c.TitleSlug,
                    RatioFromBasicMeasureUnit = c.RatioFromBasicMeasureUnit.Value
                });
            }

            var formulatedUnits = measureDimension.MeasureUnits.OfType<FormulatedMeasureUnit>().ToList();
            if (formulatedUnits != null)
            {
                dimension.FormulatedUnits = formulatedUnits.Select(f => new FormulatedMeasureUnitResponse
                {
                    Symbol = f.Id.Id,
                    Title = f.Title,
                    TitleSlug = f.TitleSlug,
                    ConvertFormulaFromBasicUnit = f.ConvertFormulaFromBasicUnit.Formula,
                    ConvertFormulaToBasicUnit = f.ConvertFormulaToBasicUnit.Formula
                });
            }

            return dimension;
        }

        public static IEnumerable<MeasureDimensionResponse> MapToQueryResponse(
            this IEnumerable<MeasureDimension> measureDimensions)
        {
            foreach (var dimension in measureDimensions)
            {
                yield return MapToQueryResponse(dimension);
            }
        }
    }
}
