using Xunit;

namespace SimplyTestData.UnitTests.Session
{
    public class WhenInitialized : TestDataSessionFixtureBase
    {
        public WhenInitialized()
        {
            Session = new TestDataSession();
        }

        [Fact]
        public void CustomizationsContainerIsNull()
        {
            Assert.Null(Session.CustomizationsContainer);
        }
    }
}
