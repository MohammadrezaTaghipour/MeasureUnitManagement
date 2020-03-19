using MeasureUnitManagement.Domain.MeasureDimensions;
using MeasureUnitManagement.Infrastructure.Query.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            if (measureDimension.BasicUnit != null)
            {
                dimension.BasicUnit = new BasicMeasureUnitResponse
                {
                    Symbol = measureDimension?.BasicUnit?.Id?.Id,
                    Title = measureDimension?.BasicUnit?.Title,
                    TitleSlug = measureDimension?.BasicUnit?.TitleSlug
                };
            }

            if (measureDimension.CoefficientUnits != null)
            {
                dimension.CoefficientUnits = measureDimension.CoefficientUnits.Select(c => new CoefficientMeasureUnitResponse
                {
                    Symbol = c.Id.Id,
                    Title = c.Title,
                    TitleSlug = c.TitleSlug,
                    RatioFromBasicMeasureUnit = c.RatioFromBasicMeasureUnit.Value
                });
            }

            if (measureDimension.FormulatedUnits != null)
            {
                dimension.FormulatedUnits = measureDimension.FormulatedUnits.Select(f => new FormulatedMeasureUnitResponse
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
