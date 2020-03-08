namespace MeasureUnitManagement.Domain.MeasureDimensions.Entities
{
    public class BasicMeasureUnit : MeasureUnit
    {
        protected BasicMeasureUnit(Symbol id, string title, string titleSlug)
            : base(id, title, titleSlug)
        { }

        public static BasicMeasureUnit Create(Symbol id, string title, string titleSlug)
        {
            var basicUnit = new BasicMeasureUnit(id, title, titleSlug);
            return basicUnit;
        }

        public new void Modify(string title, string titleSlug)
        {
            base.Modify(title, titleSlug);
        }

        public double MesaureToBasicUnit(double value)
        {
            return value * 1;
        }

        public double MesaureFromBasicUnit(double value)
        {
            return value * 1;
        }
    }
}
