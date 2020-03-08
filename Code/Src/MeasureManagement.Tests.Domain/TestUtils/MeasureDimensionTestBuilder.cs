using MeasureUnitManagement.Domain.MeasureDimensions;
using MeasureUnitManagement.Domain.MeasureDimensions.Args;
using System.Collections.Generic;
using System.Linq;

namespace MeasureManagement.Tests.Domain.TestUtils
{
    public class MeasureDimensionTestBuilder
    {
        private long _id;
        private string _title;
        private BasicMeasureUnitArg _basicMeasureUnitArg;
        private List<CoefficientMeasureUnitArg> _coefficientMeasureUnitArgs;
        private List<FormulatedMeasureUnitArg> _formulatedMeasureUnitArgs;

        public MeasureDimensionTestBuilder()
        {
            _coefficientMeasureUnitArgs = new List<CoefficientMeasureUnitArg>();
            _formulatedMeasureUnitArgs = new List<FormulatedMeasureUnitArg>();
        }

        public MeasureDimensionTestBuilder WithSymbol(long id)
        {
            this._id = id;
            return this;
        }

        public MeasureDimensionTestBuilder WithTitle(string title)
        {
            this._title = title;
            return this;
        }

        public MeasureDimensionTestBuilder WithBasicMeasureUnitArg(BasicMeasureUnitArg arg)
        {
            this._basicMeasureUnitArg = arg;
            return this;
        }

        public MeasureDimensionTestBuilder WithCoefficientMeasureUnitArg(CoefficientMeasureUnitArg arg)
        {
            _coefficientMeasureUnitArgs.Add(arg);
            return this;
        }

        public MeasureDimensionTestBuilder WithFormulatedMeasureUnitArg(FormulatedMeasureUnitArg arg)
        {
            _formulatedMeasureUnitArgs.Add(arg);
            return this;
        }

        public MeasureDimension Build()
        {
            var dimension = MeasureDimension.Create(_id, _title);
            if (_basicMeasureUnitArg != null)
                dimension.DefineBasicMeasureUnit(_basicMeasureUnitArg);
            if (_coefficientMeasureUnitArgs.Any())
                _coefficientMeasureUnitArgs.ForEach(c =>
                dimension.DefineCoefficientUnit(c));
            if (_formulatedMeasureUnitArgs.Any())
                _formulatedMeasureUnitArgs.ForEach(f =>
                dimension.DefineFormulatedUnit(f));
            return dimension;
        }
    }
}
