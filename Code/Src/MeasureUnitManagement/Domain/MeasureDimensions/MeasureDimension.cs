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

        private List<MeasureUnit> _measureUnits = new List<MeasureUnit>();
        public IReadOnlyList<MeasureUnit> MeasureUnits => _measureUnits;

        public static MeasureDimension Create(long id, string title)
        {
            var dimension = new MeasureDimension(id, title);
            return dimension;
        }

        public void DefineBasicMeasureUnit(BasicMeasureUnitArg arg)
        {
            if (arg == null)
                throw new ArgumentsForDefiningBasicMeasureUnitCannotBeNull();

            GaurdAgainstMultipleBasicUnitDefinition();

            this._measureUnits.Add(BasicMeasureUnit.Create(arg.Id, arg.Title, arg.TitleSlug));
        }

        public void ModifyBasicMeasureUnit(BasicMeasureUnitArg arg)
        {
            if (arg == null)
                throw new ArgumentsForModifyingBasicMeasureUnitCannotBeNull();

            var unit = (BasicMeasureUnit)this.FindUnitFrom(arg.Id);

            unit.Modify(arg.Title, arg.TitleSlug);
        }

        public void DefineCoefficientUnit(CoefficientMeasureUnitArg arg)
        {
            if (arg == null)
                throw new ArgumentsForDefiningCoefficientMeasureUnitCannotBeNull();

            GaurdAgainstBasicUnitShouldBeDefinedBefore();

            this._measureUnits.Add(CoefficientMeasureUnit.Create(arg.Id,
                arg.Title, arg.TitleSlug, arg.RatioFromBasicMeasureUnit));
        }

        public void ModifyCoefficientUnit(CoefficientMeasureUnitArg arg)
        {
            if (arg == null)
                throw new ArgumentsForModifyingCoefficientMeasureUnitCannotBeNull();

            var unit = (CoefficientMeasureUnit)this.FindUnitFrom(arg.Id);

            unit.Modify(arg.Title, arg.TitleSlug,
                arg.RatioFromBasicMeasureUnit);
        }

        public void DefineFormulatedUnit(FormulatedMeasureUnitArg arg)
        {
            if (arg == null)
                throw new ArgumentsForDefiningFormulatedMeasureUnitCannotBeNull();

            GaurdAgainstBasicUnitShouldBeDefinedBefore();

            this._measureUnits.Add(FormulatedMeasureUnit.Create(arg.Id, arg.Title,
                arg.TitleSlug, new ConvertFormula(arg.ConvertFormulaFromBasicUnit.Formula),
                new ConvertFormula(arg.ConvertFormulaToBasicUnit.Formula)));
        }

        public void ModifyFormulatedUnit(FormulatedMeasureUnitArg arg)
        {
            if (arg == null)
                throw new ArgumentsForDefiningFormulatedMeasureUnitCannotBeNull();

            var unit = (FormulatedMeasureUnit)this.FindUnitFrom(arg.Id);

            unit.Modify(arg.Title, arg.TitleSlug,
                new ConvertFormula(arg.ConvertFormulaFromBasicUnit.Formula),
                new ConvertFormula(arg.ConvertFormulaToBasicUnit.Formula));
        }

        public double MeasureUnitsFor(MeasurementArg arg,
            IFormulaExpressionEvaluator expressionEvaluator)
        {
            var fromMeasureUnit = this.FindUnitFrom(arg.FromUnitSymbol);
            var toMeasureUnit = this.FindUnitFrom(arg.ToUnitSymbol);

            var valueFromBasicUnit = fromMeasureUnit.MeasureToBasicUnit(arg.FromValue,
                expressionEvaluator);
            var result = toMeasureUnit.MeasureFromBasicUnit(valueFromBasicUnit, expressionEvaluator);

            return result;
        }

        private void GaurdAgainstMultipleBasicUnitDefinition()
        {
            if (this._measureUnits.OfType<BasicMeasureUnit>().Any())
                throw new BasicMeasureUnitHasBennDefinedBeforeForThisDimension();
        }

        private void GaurdAgainstBasicUnitShouldBeDefinedBefore()
        {
            if (this._measureUnits.OfType<BasicMeasureUnit>().Any() == false)
                throw new BasicMeasureUnitHasNotDefinedYetForThisDimension();
        }

        private MeasureUnit FindUnitFrom(Symbol symbol)
        {
            if (this.MeasureUnits.Any(m => m.Id == symbol))
                return this.MeasureUnits.First(m => m.Id == symbol);

            throw new InvalidMeasureUnit($"symbol: {symbol.Id}");
        }
    }
}
