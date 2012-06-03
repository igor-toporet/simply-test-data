namespace SampleDomain.Persistence
{
    public interface IVersionable
    {
        int Version { get; set; }
    }
}
