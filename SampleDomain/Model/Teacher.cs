using System.Collections.Generic;

namespace SampleDomain.Model
{
    public class Teacher : Person
    {
        public Teacher()
        {
            CourseIDs = new List<int>();
        }

        public List<int> CourseIDs { get; private set; }
    }
}
