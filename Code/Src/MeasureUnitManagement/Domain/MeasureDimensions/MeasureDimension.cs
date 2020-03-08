using MeasureUnitManagement.Domain.Exceptions;
using MeasureUnitManagement.Domain.MeasureDimensions.Args;
using MeasureUnitManagement.Domain.MeasureDimensions.Entities;
using MeasureUnitManagement.Domain.MeasureDimensions.ValueObjects;
using MeasureUnitManagement.Domain.Services.ExpressionEvaluator;
using MeasureUnitManagement.Infrastructure.Core;
using System.Collections.Generic;
using System.Linq;

namespace MeasureUnitManagement.Domain.MeasureDimensions
{
    public class MeasureDimension : AggregateRoot<long>
    {
        protected MeasureDimension(long id, string title)
        {
            this.Id = id;
            this.Title = title;
        }

        public string Title { get; private set; }
        public BasicMeasureUnit BasicUnit { get; private set; }

        private readonly List<CoefficientMeasureUnit> _coefficientUnits = new List<CoefficientMeasureUnit>();
        public IReadOnlyList<CoefficientMeasureUnit> CoefficientUnits => _coefficientUnits.AsReadOnly();

        private readonly List<FormulatedMeasureUnit> _formulatedUnits = new List<FormulatedMeasureUnit>();
        public IReadOnlyList<FormulatedMeasureUnit> FormulatedUnits => _formulatedUnits.AsReadOnly();

        public static MeasureDimension Create(long id, string title)
        {
            var dimension = new MeasureDimension(id, title);
            return dimension;
        }

        public void DefineBasicMeasureUnit(BasicMeasureUnitArg arg)
        {
            if (arg == null)
                throw new ArgumentsForDefiningBasicMeasureUnitCannotBeNull();

            if (BasicUnit != null)
                return;

            BasicUnit = BasicMeasureUnit.Create(arg.Id, arg.Title, arg.TitleSlug);
        }

        public void ModifyBasicMeasureUnit(BasicMeasureUnitArg arg)
        {
            if (arg == null)
                throw new ArgumentsForModifyingBasicMeasureUnitCannotBeNull();

            if (BasicUnit == null)
                return;

            BasicUnit.Modify(arg.Title, arg.TitleSlug);
        }

        public void DefineCoefficientUnit(CoefficientMeasureUnitArg arg)
        {
            if (arg == null)
                throw new ArgumentsForDefiningCoefficientMeasureUnitCannotBeNull();

            if (this.BasicUnit == null)
                throw new BasicMeasureUnitHasNotDefinedYetForThisDimension();

            var unit = CoefficientMeasureUnit.Create(arg.Id,
                arg.Title, arg.TitleSlug, arg.RatioFromBasicMeasureUnit);
            _coefficientUnits.Add(unit);
        }

        public void ModifyCoefficientUnit(CoefficientMeasureUnitArg arg)
        {
            if (arg == null)
                throw new ArgumentsForModifyingCoefficientMeasureUnitCannotBeNull();

            var unit = _coefficientUnits.Find(u => u.Id == arg.Id);

            unit.Modify(arg.Title, arg.TitleSlug,
                arg.RatioFromBasicMeasureUnit);
        }

        public void DefineFormulatedUnit(FormulatedMeasureUnitArg arg)
        {
            if (arg == null)
                throw new ArgumentsForDefiningFormulatedMeasureUnitCannotBeNull();

            var unit = FormulatedMeasureUnit.Create(arg.Id, arg.Title,
                arg.TitleSlug, new ConvertFormula(arg.ConvertFormulaFromBasicUnit.Formula),
                new ConvertFormula(arg.ConvertFormulaToBasicUnit.Formula));
            _formulatedUnits.Add(unit);
        }

        public void ModifyFormulatedUnit(FormulatedMeasureUnitArg arg)
        {
            if (arg == null)
                throw new ArgumentsForDefiningFormulatedMeasureUnitCannotBeNull();

            var unit = _formulatedUnits.Find(u => u.Id == arg.Id);

            unit.Modify(arg.Title, arg.TitleSlug,
                new ConvertFormula(arg.ConvertFormulaFromBasicUnit.Formula),
                new ConvertFormula(arg.ConvertFormulaToBasicUnit.Formula));
        }

        public double MeasureUnitsFor(MeasurementArg arg,
            IFormulaExpressionEvluator expressionEvluator)
        {
            var fromMeasureUnit = this.FindUnitFrom(arg.FromUnitSymbol);
            var toMeasureUnit = this.FindUnitFrom(arg.ToUnitSymbol);

            double valueFromBasicUnit = this.MeasureToBasicUnit(fromMeasureUnit,
                arg.FromValue, expressionEvluator);
            var result = this.MeasureFromBasicUnit(toMeasureUnit, valueFromBasicUnit,
                expressionEvluator);
            return result;
        }

        private MeasureUnit FindUnitFrom(Symbol symbol)
        {
            if (BasicUnit.Id.Id == symbol.Id)
                return BasicUnit;

            if (CoefficientUnits.Any(u => u.Id == symbol))
                return CoefficientUnits.First(u => u.Id.Id == symbol.Id);

            if (FormulatedUnits.Any(u => u.Id == symbol))
                return FormulatedUnits.First(u => u.Id.Id == symbol.Id);

            throw new InvalidMeasureUnit($"symbol: {symbol.Id}");
        }

        private double MeasureFromBasicUnit(MeasureUnit unit, double value,
            IFormulaExpressionEvluator expressionEvluator)
        {
            if (unit is BasicMeasureUnit basicUnit)
                return basicUnit.MesaureFromBasicUnit(value);

            if (unit is CoefficientMeasureUnit coeffientUnit)
                return coeffientUnit.MesaureFromBasicUnit(value);

            if (unit is FormulatedMeasureUnit formulatedUnit)
                return formulatedUnit.MesaureFromBasicUnit(value, expressionEvluator);

            throw new InvalidMeasureUnit($"type: {unit.GetType()}");
        }

        private double MeasureToBasicUnit(MeasureUnit unit, double value,
            IFormulaExpressionEvluator expressionEvluator)
        {
            if (unit is BasicMeasureUnit basicUnit)
                return basicUnit.MesaureToBasicUnit(value);

            if (unit is CoefficientMeasureUnit coeffientUnit)
                return coeffientUnit.MesaureToBasicUnit(value);

            if (unit is FormulatedMeasureUnit formulatedUnit)
                return formulatedUnit.MesaureToBasicUnit(value, expressionEvluator);

            throw new InvalidMeasureUnit($"type: {unit.GetType()}");
        }
    }
}
