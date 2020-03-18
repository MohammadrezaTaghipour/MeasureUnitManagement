using MeasureManagement.Tests.Domain.TestData;
using MeasureManagement.Tests.Domain.TestUtils;
using MeasureUnitManagement.Domain.Exceptions;
using MeasureUnitManagement.Domain.MeasureDimensions;
using MeasureUnitManagement.Domain.MeasureDimensions.Args;
using MeasureUnitManagement.Domain.Services.ExpressionEvaluator;
using NFluent;
using NSubstitute;
using System;
using System.Linq;
using Xunit;

namespace MeasureUnitManagement.Tests.Domain.MeasureDimensions
{
    public class MeasureDimensionTests
    {
        private readonly IFormulaExpressionEvaluator _formulaExpressionEvaluator;

        public MeasureDimensionTests()
        {
            _formulaExpressionEvaluator = Substitute.For<IFormulaExpressionEvaluator>();
        }

        [Fact]
        public void for_measure_dimension_new_basic_unit_should_be_defined_properly()
        {
            var dimension = new MeasureDimensionTestBuilder()
                            .WithTitle("طول")
                            .Build();

            var symbol = new Symbol(MeasureUnitSymbolTestData.MeterUnitSymbol);
            var basicUnitArg = new BasicMeasureUnitTestBuilder()
                .WithSymbol(symbol)
                .WithTitle("متر")
                .WithTitleSlug("Meter")
                .BuildArg();

            dimension.DefineBasicMeasureUnit(basicUnitArg);

            Check.That(dimension.BasicUnit).Considering()
                .All.Properties.IsEqualTo(basicUnitArg);
        }

        [Fact]
        public void measure_dimension_should_modify_basic_unit_properly()
        {
            var basicUnitBuilder = new BasicMeasureUnitTestBuilder();
            var dimension = new MeasureDimensionTestBuilder()
                .WithBasicMeasureUnitArg(basicUnitBuilder.BuildArg())
                .Build();

            var symbol = new Symbol(MeasureUnitSymbolTestData.MeterUnitSymbol);
            var modifArg = basicUnitBuilder
                .WithSymbol(symbol)
                .WithTitle("متر جدید")
                .WithTitleSlug("NewMeter")
                .BuildArg();

            dimension.ModifyBasicMeasureUnit(modifArg);

            Check.That(dimension.BasicUnit).Considering()
                .All.Properties.IsEqualTo(modifArg);
        }

        [Fact]
        public void measure_dimension_should_measure_basic_units_properly()
        {
            var symbol = new Symbol(MeasureUnitSymbolTestData.MeterUnitSymbol);
            var basicUnitArg = new BasicMeasureUnitTestBuilder()
                .WithSymbol(symbol)
                .WithTitle("متر")
                .WithTitleSlug("Meter")
                .BuildArg();

            var dimension = new MeasureDimensionTestBuilder()
                .WithTitle("طول")
                .WithBasicMeasureUnitArg(basicUnitArg)
                .Build();

            var arg = new MeasurementArg
            {
                FromValue = 22.5,
                FromUnitSymbol = symbol,
                ToUnitSymbol = symbol
            };

            var expected = 22.5;

            var mesaureValue = dimension
                .MeasureUnitsFor(arg, _formulaExpressionEvaluator);

            Check.That(mesaureValue).IsEqualTo(expected);
        }

        [Fact]
        public void for_measure_dimension_defining_new_coeffiecient_units_should_throw_exception_when_basic_unit_is_not_defined_befor()
        {
            var centiMeter = new CoefficientMeasureUnitTestBuilder()
                .WithSymbol(new Symbol(MeasureUnitSymbolTestData.CentiMeterUnitSymbol))
                .WithTitle("سانتیمتر")
                .WithRatioFromBasicMeasureUnit(0.01)
                .BuildArg();

            var dimension = new MeasureDimensionTestBuilder()
                .WithTitle("طول")
                .Build();

            Action action = () => dimension.DefineCoefficientUnit(centiMeter);

            Check.ThatCode(action)
                .Throws<BasicMeasureUnitHasNotDefinedYetForThisDimension>();
        }

        [Fact]
        public void for_measure_dimension_new_coeffiecient_units_should_be_defined_properly()
        {
            var meter = new BasicMeasureUnitTestBuilder()
                .WithSymbol(new Symbol(MeasureUnitSymbolTestData.MeterUnitSymbol))
                .WithTitle("متر")
                .BuildArg();

            var centiMeterBuilder = new CoefficientMeasureUnitTestBuilder();
            var centiMeter = centiMeterBuilder
                .WithSymbol(new Symbol(MeasureUnitSymbolTestData.CentiMeterUnitSymbol))
                .WithTitle("سانتیمتر")
                .WithRatioFromBasicMeasureUnit(0.01)
                .BuildArg();

            var dimension = new MeasureDimensionTestBuilder()
                .WithBasicMeasureUnitArg(meter)
                .Build();

            dimension.DefineCoefficientUnit(centiMeter);

            Check.That(dimension.CoefficientUnits.First(c => c.Id == centiMeter.Id))
                .Considering().All.Properties.IsEqualTo(centiMeter);
        }

        [Fact]
        public void measure_dimension_should_modify_coeffiecient_units_properly()
        {
            var basicUnit = new BasicMeasureUnitTestBuilder().BuildArg();

            var coefficientUnitBuilder = new CoefficientMeasureUnitTestBuilder();
            var coefficientUnit = coefficientUnitBuilder.BuildArg();

            var dimension = new MeasureDimensionTestBuilder()
                .WithBasicMeasureUnitArg(basicUnit)
                .WithCoefficientMeasureUnitArg(coefficientUnit)
                .Build();

            var arg = coefficientUnitBuilder
                .WithTitle("new title")
                .WithTitleSlug("new slug")
                .WithRatioFromBasicMeasureUnit(0.0226)
                .BuildArg();

            dimension.ModifyCoefficientUnit(arg);

            Check.That(dimension.CoefficientUnits.First(c => c.Id == arg.Id))
                .Considering().All.Properties.IsEqualTo(arg);
        }

        [Fact]
        public void measure_dimension_should_measure_coeffiecient_units_to_coeffiecient_units_properly()
        {
            var meter = new BasicMeasureUnitTestBuilder()
                .WithSymbol(new Symbol(MeasureUnitSymbolTestData.MeterUnitSymbol))
                .WithTitle("متر")
                .BuildArg();

            var centiMeter = new CoefficientMeasureUnitTestBuilder()
                .WithSymbol(new Symbol(MeasureUnitSymbolTestData.CentiMeterUnitSymbol))
                .WithTitle("سانتیمتر")
                .WithRatioFromBasicMeasureUnit(0.01)
                .BuildArg();

            var kiloMeter = new CoefficientMeasureUnitTestBuilder()
                .WithSymbol(new Symbol(MeasureUnitSymbolTestData.KiloMeterUnitSymbol))
                .WithTitle("کیلومتر")
                .WithRatioFromBasicMeasureUnit(1000)
                .BuildArg();

            var dimension = new MeasureDimensionTestBuilder()
                .WithTitle("طول")
                .WithBasicMeasureUnitArg(meter)
                .WithCoefficientMeasureUnitArg(centiMeter)
                .WithCoefficientMeasureUnitArg(kiloMeter)
                .Build();

            var arg = new MeasurementArg
            {
                FromValue = 10,
                FromUnitSymbol = new Symbol(MeasureUnitSymbolTestData.KiloMeterUnitSymbol),
                ToUnitSymbol = new Symbol(MeasureUnitSymbolTestData.CentiMeterUnitSymbol)
            };

            var expected = 1000000;

            var measuredValue = dimension.MeasureUnitsFor(arg, _formulaExpressionEvaluator);

            Check.That(measuredValue).IsEqualTo(expected);
        }

        [Fact]
        public void for_measure_dimension_new_formulated_units_should_be_defined_properly()
        {
            var celcius = new BasicMeasureUnitTestBuilder()
                .WithSymbol(new Symbol(MeasureUnitSymbolTestData.CelciusUnitSymbol))
                .WithTitle("سلسیوس")
                .BuildArg();

            var formulatedUnitBuilder = new FormulatedMeasureUnitTestBuilder();
            var farenheit = formulatedUnitBuilder
                .WithSymbol(new Symbol(MeasureUnitSymbolTestData.FarenheitUnitSymbol))
                .WithTitle("فارنهایت")
                .WithConvertFormulaToBasicUnit("my formula")
                .WitCconvertFormulaFromBasicUnit("my formula")
                .BuildArg();

            var dimension = new MeasureDimensionTestBuilder()
                .WithBasicMeasureUnitArg(celcius)
                .Build();

            dimension.DefineFormulatedUnit(farenheit);

            Check.That(dimension.FormulatedUnits.First(c => c.Id == farenheit.Id))
                .Considering().All.Properties.IsEqualTo(farenheit);
        }

        [Fact]
        public void
            for_measure_dimension_defining_new_coeffiecient_unit_should_throw_exception_when_formula_has_invliad_parenthesis_formart()
        {
            var basicUnit = new BasicMeasureUnitTestBuilder().BuildArg();

            var formulatedUnitBuilder = new FormulatedMeasureUnitTestBuilder();
            var farenheit = formulatedUnitBuilder
                .WithConvertFormulaToBasicUnit("23 + ( (a+3)")
                .BuildArg();

            var dimension = new MeasureDimensionTestBuilder()
                .WithBasicMeasureUnitArg(basicUnit)
                .Build();

            Action action = () => dimension.DefineFormulatedUnit(farenheit);

            Check.ThatCode(action).Throws<ParenthesisAreNotBalanced>();
        }


        [Fact]
        public void measure_dimension_should_modify_formulated_units_properly()
        {
            var basicUnit = new BasicMeasureUnitTestBuilder().BuildArg();

            var formulatedUnitBuilder = new FormulatedMeasureUnitTestBuilder();
            var formulatedUnit = formulatedUnitBuilder.BuildArg();

            var dimension = new MeasureDimensionTestBuilder()
                .WithBasicMeasureUnitArg(basicUnit)
                .WithFormulatedMeasureUnitArg(formulatedUnit)
                .Build();

            var arg = formulatedUnitBuilder
                .WithTitle("new title")
                .WithTitleSlug("new slug")
                .WitCconvertFormulaFromBasicUnit("changed formula")
                .BuildArg();

            dimension.ModifyFormulatedUnit(arg);

            Check.That(dimension.FormulatedUnits.First(c => c.Id == arg.Id))
                .Considering().All.Properties.IsEqualTo(arg);
        }

        [Theory]
        [InlineData(32, 0)]
        [InlineData(41, 5)]
        [InlineData(55.5, 13.055555555555555)]
        public void measure_dimension_should_measure_formulated_units_to_basic_unit_properly(
            double fromValue, double expected)
        {
            var celcius = new BasicMeasureUnitTestBuilder()
                .WithSymbol(new Symbol(MeasureUnitSymbolTestData.CelciusUnitSymbol))
                .WithTitle("سلسیوس")
                .BuildArg();

            var formulatedUnitBuilder = new FormulatedMeasureUnitTestBuilder();
            var farenheit = formulatedUnitBuilder
                .WithSymbol(new Symbol(MeasureUnitSymbolTestData.FarenheitUnitSymbol))
                .WithTitle("فارنهایت")
                .WithConvertFormulaToBasicUnit("5 / 9 * (a - 32)")
                .WitCconvertFormulaFromBasicUnit("32 + (9 / 5 * a)")
                .BuildArg();

            var dimension = new MeasureDimensionTestBuilder()
                .WithBasicMeasureUnitArg(celcius)
                .WithFormulatedMeasureUnitArg(farenheit)
                .Build();

            var arg = new MeasurementArg
            {
                FromValue = fromValue,
                FromUnitSymbol = new Symbol(MeasureUnitSymbolTestData.FarenheitUnitSymbol),
                ToUnitSymbol = new Symbol(MeasureUnitSymbolTestData.CelciusUnitSymbol)
            };

            var measuredValue = dimension.MeasureUnitsFor(arg, new FormulaExpressionEvaluator());

            Check.That(measuredValue).IsEqualTo(expected);
        }

        [Theory]
        [InlineData(273, 31.73000000000004)]
        [InlineData(433.1, 319.9100000000001)]
        public void measure_dimension_should_measure_formulated_units_to_formulated_units_properly(
            double fromValue, double expected)
        {
            var celcius = new BasicMeasureUnitTestBuilder()
                .WithSymbol(new Symbol(MeasureUnitSymbolTestData.CelciusUnitSymbol))
                .WithTitle("سلسیوس")
                .BuildArg();

            var formulatedUnitBuilder = new FormulatedMeasureUnitTestBuilder();
            var farenheit = formulatedUnitBuilder
                .WithSymbol(new Symbol(MeasureUnitSymbolTestData.FarenheitUnitSymbol))
                .WithTitle("فارنهایت")
                .WithConvertFormulaToBasicUnit("5 / 9 * (a - 32)")
                .WitCconvertFormulaFromBasicUnit("32 + (9 / 5 * a)")
                .BuildArg();

            var kelvin = formulatedUnitBuilder
                .WithSymbol(new Symbol(MeasureUnitSymbolTestData.KelvinUnitSymbol))
                .WithTitle("کلوین")
                .WithConvertFormulaToBasicUnit("a - 273.15")
                .WitCconvertFormulaFromBasicUnit("273.15 + a")
                .BuildArg();

            var dimension = new MeasureDimensionTestBuilder()
                .WithBasicMeasureUnitArg(celcius)
                .WithFormulatedMeasureUnitArg(farenheit)
                .WithFormulatedMeasureUnitArg(kelvin)
                .Build();

            var arg = new MeasurementArg
            {
                FromValue = fromValue,
                FromUnitSymbol = new Symbol(MeasureUnitSymbolTestData.KelvinUnitSymbol),
                ToUnitSymbol = new Symbol(MeasureUnitSymbolTestData.FarenheitUnitSymbol)
            };

            var measuredValue = dimension.MeasureUnitsFor(arg, new FormulaExpressionEvaluator());

            Check.That(measuredValue).IsEqualTo(expected);
        }

    }
}
