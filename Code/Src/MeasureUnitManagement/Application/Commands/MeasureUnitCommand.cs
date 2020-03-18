using MediatR;

namespace MeasureUnitManagement.Application.Commands
{
    public class MeasureUnitCommand : IRequest<double>
    {
        public long DimensionId { get; set; } 
        public double Value { get; set; }
        public string FromUnitSymbol { get; set; }
        public string ToUnitSymbol { get; set; }
    }
}
