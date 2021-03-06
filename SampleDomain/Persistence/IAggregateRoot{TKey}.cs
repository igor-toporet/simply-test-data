﻿namespace SampleDomain.Persistence
{
    public interface IAggregateRoot<TKey> : IIdentifiable<TKey>, IVersionable where TKey : struct
    {
    }
}
