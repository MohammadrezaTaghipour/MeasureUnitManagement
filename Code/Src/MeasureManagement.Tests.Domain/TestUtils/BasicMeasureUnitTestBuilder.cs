using MeasureManagement.Tests.Domain.TestData;
using MeasureUnitManagement.Domain.MeasureDimensions;
using MeasureUnitManagement.Domain.MeasureDimensions.Args;
using MeasureUnitManagement.Domain.MeasureDimensions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeasureManagement.Tests.Domain.TestUtils
{
    public class BasicMeasureUnitTestBuilder
    {
        public BasicMeasureUnitTestBuilder()
        {
            _symbol = new Symbol(MeasureUnitSymbolTestData.MeterUnitSymbol);
            _title = "متر";
            _titleSlug = "Meter";
        }

        private Symbol _symbol;
        private string _title;
        private string _titleSlug;

        public BasicMeasureUnitTestBuilder WithSymbol(Symbol symbol)
        {
            this._symbol = symbol;
            return this;
        }

        public BasicMeasureUnitTestBuilder WithTitle(string title)
        {
            this._title = title;
            return this;
        }

        public BasicMeasureUnitTestBuilder WithTitleSlug(string slug)
        {
            this._titleSlug = slug;
            return this;
        }

        public BasicMeasureUnit Build()
        {
            return BasicMeasureUnit.Create(_symbol, _title, _titleSlug);
        }

        public BasicMeasureUnitArg BuildArg()
        {
            return new BasicMeasureUnitArg
            {
                Id = _symbol,
                Title = _title,
                TitleSlug = _titleSlug
            };
        }
    }
}
