using System;
using System.Threading.Tasks;

namespace MeasureUnitManagement.Infrastructure.DataAccess
{
    public interface ISequenceIdGenerator
    {
        Task<long> GetNextSequence<TAggregate>();
    }
}
