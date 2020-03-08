using System;

namespace MeasureUnitManagement.Infrastructure.Core
{
    public abstract class EntityId<T> 
    {
        public T Id { get; set; }
    }
}
