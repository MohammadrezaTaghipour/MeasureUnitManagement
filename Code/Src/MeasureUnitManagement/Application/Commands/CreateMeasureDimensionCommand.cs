using MediatR;

namespace MeasureUnitManagement.Application.Commands
{
    public class CreateMeasureDimensionCommand : IRequest<long>
    {
        public string Title { get; set; }
    }
}
