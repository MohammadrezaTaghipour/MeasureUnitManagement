using System;

namespace MeasureUnitManagement.Infrastructure.Core
{
    public abstract class AggregateRoot<T> 
    {
        public T Id { get; protected set; }
    }
}
