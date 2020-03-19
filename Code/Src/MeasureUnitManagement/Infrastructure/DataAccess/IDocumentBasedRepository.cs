using MeasureUnitManagement.Infrastructure.Core;
using System.Threading;
using System.Threading.Tasks;

namespace MeasureUnitManagement.Infrastructure.DataAccess
{
    public interface IDocumentBasedRepository<TKey, TAggregate>
        where TAggregate : AggregateRoot<TKey>
    {
        Task Add(TAggregate aggregate, CancellationToken cancellationToken);
        Task UpSert(TAggregate aggregate, CancellationToken cancellationToken);
        Task<TAggregate> GetById(TKey id);
        Task<long> GetNextId();
    }
}
