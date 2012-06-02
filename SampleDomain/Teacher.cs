using System.Collections.Generic;

namespace SampleDomain
{
    public class Teacher : Person
    {
        protected Teacher()
        {
            Courses = new List<Course>();
        }

        public List<Course> Courses { get; private set; }
    }
}
