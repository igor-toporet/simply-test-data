using FluentAssertions;
using Xunit;

namespace SimplyTestData.UnitTests.Container
{
    public class WhenDuplicateCustomizationsAreAllowed : DuplicateCustomizationsFixtureBase
    {
        public WhenDuplicateCustomizationsAreAllowed()
        {
            Container.AllowDuplicates = true;

            Container.AddForType(IncreaseMemberBySeven);
            Container.AddForType(IncreaseMemberBySeven);
            Container.AddForType(IncreaseMemberBySeven);
        }

        [Fact]
        public void AllDuplicateCustomizationsAreReturned()
        {
            var c = new C();
            var customizations = Container.GetApplicableToType<C>();

            foreach (var action in customizations)
            {
                action(c);
            }

            c.M.Should().Be(21);
        }
    }
}
