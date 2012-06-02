using System.Collections.Generic;

namespace SampleDomain
{
    public class Student : Person
    {
        protected Student()
        {
            AttendedCourses = new List<AttendedCourse>();
        }

        public List<AttendedCourse> AttendedCourses { get; private set; }
    }
}
