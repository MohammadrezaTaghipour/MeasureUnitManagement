namespace MeasureUnitManagement.Infrastructure.Query.Models
{
    public class CoefficientMeasureUnitResponse
    {
        public string Symbol { get; set; }
        public string Title { get; set; }
        public string TitleSlug { get; set; }
        public double RatioFromBasicMeasureUnit { get; set; }
    }
}
