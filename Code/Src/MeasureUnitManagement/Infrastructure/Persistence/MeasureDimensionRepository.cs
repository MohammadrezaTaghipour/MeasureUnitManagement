using MeasureUnitManagement.Domain.MeasureDimensions;
using MeasureUnitManagement.Infrastructure.DataAccess;
using System.Threading;
using System.Threading.Tasks;

namespace MeasureUnitManagement.Infrastructure.Persistence
{
    public class MeasureDimensionRepository : IMeasureDimensionRepository
    {
        private readonly IDocumentBasedRepository<long, MeasureDimension> _repository;

        public MeasureDimensionRepository(
            IDocumentBasedRepository<long, MeasureDimension> repository)
        {
            this._repository = repository;
        }

        public Task Add(MeasureDimension measureDimension, CancellationToken cancellationToken)
        {
            return _repository.Add(measureDimension, cancellationToken);
        }

        public Task<MeasureDimension> GetById(long id)
        {
            return _repository.GetById(id);
        }

        public Task<long> GetNextId()
        {
            return _repository.GetNextId();
        }
    }
}
