using System.Collections.Generic;

namespace SampleDomain.Model
{
    public class Student : Person
    {
        public Student()
        {
            AttendedCourses = new List<AttendedCourse>();
        }

        public List<AttendedCourse> AttendedCourses { get; private set; }
    }
}
