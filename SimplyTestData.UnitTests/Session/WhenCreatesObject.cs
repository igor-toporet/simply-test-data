using FakeItEasy;
using SampleDomain.Model;
using Xunit;

namespace SimplyTestData.UnitTests.Session
{
    public class WhenCreatesObject : TestDataSessionFixtureBase
    {
        public WhenCreatesObject()
        {
            Session = new TestDataSession();
        }

        [Fact]
        public void AsksContainerForApplicableCustomizations()
        {
            var fakeCustomizationsContainer = A.Fake<ICustomizationsContainer>();
            Session.Customizations = fakeCustomizationsContainer;

            Session.Create<Student>(s => s.FirstName = "test");

            A.CallTo(() => fakeCustomizationsContainer.GetApplicableToType<Student>())
                .MustHaveHappened();
        }
    }
}
