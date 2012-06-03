namespace SampleDomain.Persistence
{
    public interface IIdentifiable<TKey> where TKey: struct
    {
        TKey Id { get; set; }
    }
}
