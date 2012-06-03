using SampleDomain.Persistence;

namespace SampleDomain.Model
{
    public class Room : IAggregateRoot<int>
    {
        int IIdentifiable<int>.Id { get; set; }

        int IVersionable.Version { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public string Location { get; set; }
    }
}
