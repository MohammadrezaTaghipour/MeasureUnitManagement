using MeasureUnitManagement.Domain.MeasureDimensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MeasureUnitManagement.Infrastructure.Persistence
{
    public class MeasureDimensionRepository : IMeasureDimensionRepository
    {
        public MeasureDimensionRepository()
        {

        }

        public Task Add(MeasureDimension measureDimension, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<MeasureDimension> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<long> GetNextId()
        {
            throw new NotImplementedException();
        }
    }
}
