using System.Linq;
using FluentAssertions;
using Xunit;

namespace SimplyTestData.UnitTests.Container
{
    public class WhenDuplicateCustomizationsAreNotAllowed : DuplicateCustomizationsFixtureBase
    {
        public WhenDuplicateCustomizationsAreNotAllowed()
        {
            Container.AllowDuplicates = false;

            Container.AddForType(IncreaseMemberBySeven);
            Container.AddForType(IncreaseMemberBySeven);
            Container.AddForType(IncreaseMemberBySeven);
        }

        [Fact]
        public void NoDuplicateCustomizationsAreReturned()
        {
            var actual = new C();
            var expected = new C();
            IncreaseMemberBySeven(expected);

            var customizations = Container.GetApplicableToType<C>().ToList();

            customizations.Should().HaveCount(1);

            customizations.ForEach(act => act(actual));
            actual.M.Should().Be(expected.M);
        }
    }
}
