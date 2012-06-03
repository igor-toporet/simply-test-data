using SampleDomain.Model;
using Xunit;

namespace SimplyTestData.UnitTests
{
    public class WhenPersistentCustomizationDefinedForBaseClass
    {
        public WhenPersistentCustomizationDefinedForBaseClass()
        {
            TestData.ClearAllPermanentCustomizations();
            TestData.SetPermanentCustomizations<Person>(Eminem.MyNameIs);
        }

        [Fact]
        public void ItIsAppliedToCreatedObjectOfDerivedClass()
        {
            var student = TestData.Create<Student>();

            Assert.Equal(Eminem.Slim, student.FirstName);
            Assert.Equal(Eminem.Shady, student.LastName);
        }

        private static class Eminem
        {
            public const string Slim = "Slim";
            public const string Shady = "Shady";

            public static void MyNameIs(Person person)
            {
                person.FirstName = Slim;
                person.LastName = Shady;
            }
        }
    }
}
