using Xunit;

namespace SimplyTestData.UnitTests.Session
{
    public class WhenInitialized : TestDataSessionFixtureBase
    {
        [Fact]
        public void CustomizationsContainerIsNull()
        {
            Assert.Null(Session.Customizations);
        }
    }
}
