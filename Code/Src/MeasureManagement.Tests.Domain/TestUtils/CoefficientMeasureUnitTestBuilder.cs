using MeasureManagement.Tests.Domain.TestData;
using MeasureUnitManagement.Domain.MeasureDimensions;
using MeasureUnitManagement.Domain.MeasureDimensions.Args;
using MeasureUnitManagement.Domain.MeasureDimensions.Entities;
using MeasureUnitManagement.Domain.MeasureDimensions.ValueObjects;

namespace MeasureManagement.Tests.Domain.TestUtils
{
    public class CoefficientMeasureUnitTestBuilder
    {
        private Symbol _symbol;
        private string _title;
        private string _titleSlug;
        private RatioFromBasicMeasureUnit _ratioFromBasicMeasureUnit;

        public CoefficientMeasureUnitTestBuilder()
        {
            this._symbol = new Symbol(MeasureUnitSymbolTestData.CentiMeterUnitSymbol);
            this._title = "سانتیمتر";
            this._titleSlug = "CentiMeter";
            this._ratioFromBasicMeasureUnit = new RatioFromBasicMeasureUnit(0.01);
        }

        public CoefficientMeasureUnitTestBuilder WithSymbol(Symbol symbol)
        {
            this._symbol = symbol;
            return this;
        }

        public CoefficientMeasureUnitTestBuilder WithTitle(string title)
        {
            this._title = title;
            return this;
        }

        public CoefficientMeasureUnitTestBuilder WithTitleSlug(string slug)
        {
            this._titleSlug = slug;
            return this;
        }

        public CoefficientMeasureUnitTestBuilder WithRatioFromBasicMeasureUnit(double ratio)
        {
            _ratioFromBasicMeasureUnit = new RatioFromBasicMeasureUnit(ratio);
            return this;
        }

        public CoefficientMeasureUnitArg BuildArg()
        {
            return new CoefficientMeasureUnitArg
            {
                Id = _symbol,
                Title = _title,
                TitleSlug = _titleSlug,
                RatioFromBasicMeasureUnit = _ratioFromBasicMeasureUnit
            };
        }

        public CoefficientMeasureUnit Build()
        {
            return CoefficientMeasureUnit.Create(_symbol, _title,
                _titleSlug, _ratioFromBasicMeasureUnit);
        }
    }
}
