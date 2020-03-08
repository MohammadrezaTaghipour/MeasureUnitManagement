namespace MeasureUnitManagement.Domain.MeasureDimensions.Args
{
    public class MeasurementArg
    {
        public double FromValue { get; set; }
        public Symbol FromUnitSymbol { get; set; }
        public Symbol ToUnitSymbol { get; set; }
    }
}
