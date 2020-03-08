using System;

namespace MeasureUnitManagement.Infrastructure.Core
{
    public abstract class Entity<T>
    {
        public T Id { get; protected set; }
    }
}
