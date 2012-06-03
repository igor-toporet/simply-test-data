using System.Collections.Generic;

namespace SampleDomain.Model
{
    public class Teacher : Person
    {
        protected Teacher()
        {
            CourseIDs = new List<int>();
        }

        public List<int> CourseIDs { get; private set; }
    }
}
