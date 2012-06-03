using SampleDomain.Persistence;

namespace SampleDomain.Model
{
    public abstract class Person : IAggregateRoot<int>
    {
        int IIdentifiable<int>.Id { get; set; }

        int IVersionable.Version { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
