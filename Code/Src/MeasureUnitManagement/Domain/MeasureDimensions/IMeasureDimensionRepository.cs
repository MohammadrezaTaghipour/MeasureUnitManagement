using System.Threading;
using System.Threading.Tasks;
using MeasureUnitManagement.Infrastructure.Core;

namespace MeasureUnitManagement.Domain.MeasureDimensions
{
    public interface IMeasureDimensionRepository : IRepository
    {
        Task<long> GetNextId();
        Task<MeasureDimension> GetById(long id);
        Task Add(MeasureDimension measureDimension, CancellationToken cancellationToken);
    }
}
