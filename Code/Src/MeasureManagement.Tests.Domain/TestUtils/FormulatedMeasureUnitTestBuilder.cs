using MeasureManagement.Tests.Domain.TestData;
using MeasureUnitManagement.Domain.MeasureDimensions;
using MeasureUnitManagement.Domain.MeasureDimensions.Args;
using MeasureUnitManagement.Domain.MeasureDimensions.Entities;
using MeasureUnitManagement.Domain.MeasureDimensions.ValueObjects;

namespace MeasureManagement.Tests.Domain.TestUtils
{
    public class FormulatedMeasureUnitTestBuilder
    {
        private Symbol _symbol;
        private string _title;
        private string _titleSlug;
        private ConvertFormulaArg _convertFormulaFromBasicUnitArg;
        private ConvertFormulaArg _convertFormulaToBasicUnitArg;

        public FormulatedMeasureUnitTestBuilder()
        {
            _symbol = new Symbol(MeasureUnitSymbolTestData.FarenheitUnitSymbol);
            _title = "فارنهایت";
            _titleSlug = "Farenheit";
            _convertFormulaFromBasicUnitArg = new ConvertFormulaArg { Formula = "32 + (9.5 * a)" };
            _convertFormulaToBasicUnitArg = new ConvertFormulaArg { Formula = "5.9 * (a - 32)" };
        }

        public FormulatedMeasureUnitTestBuilder WithSymbol(Symbol symbol)
        {
            this._symbol = symbol;
            return this;
        }

        public FormulatedMeasureUnitTestBuilder WithTitle(string title)
        {
            this._title = title;
            return this;
        }

        public FormulatedMeasureUnitTestBuilder WithTitleSlug(string slug)
        {
            this._titleSlug = slug;
            return this;
        }

        public FormulatedMeasureUnitTestBuilder WitCconvertFormulaFromBasicUnit(
            string formula)
        {
            this._convertFormulaFromBasicUnitArg = new ConvertFormulaArg
            { Formula = formula };
            return this;
        }

        public FormulatedMeasureUnitTestBuilder WithConvertFormulaToBasicUnit(
            string formula)
        {
            this._convertFormulaToBasicUnitArg = new ConvertFormulaArg
            { Formula = formula };
            return this;
        }

        public FormulatedMeasureUnitArg BuildArg()
        {
            return new FormulatedMeasureUnitArg
            {
                Id = _symbol,
                Title = _title,
                TitleSlug = _titleSlug,
                ConvertFormulaFromBasicUnit = _convertFormulaFromBasicUnitArg,
                ConvertFormulaToBasicUnit = _convertFormulaToBasicUnitArg
            };
        }

        public FormulatedMeasureUnit Build()
        {
            return FormulatedMeasureUnit.Create(_symbol, _title,
                _titleSlug, new ConvertFormula(_convertFormulaFromBasicUnitArg.Formula),
                new ConvertFormula(_convertFormulaToBasicUnitArg.Formula));
        }
    }
}
