using System.Collections.Generic;
using System.Linq;
using SampleDomain.Persistence;

namespace SampleDomain.Model
{
    public class Course : IAggregateRoot<int>
    {
        public Course()
        {
            PreferredRoomIDs = new List<int>();
        }

        int IIdentifiable<int>.Id { get; set; }

        int IVersionable.Version { get; set; }

        public string Discipline { get; set; }

        public string Name { get; set; }

        public List<int> PreferredRoomIDs { get; private set; }

        public bool HasRoomRestrictions
        {
            get { return PreferredRoomIDs.Any(); }
        }
    }
}
