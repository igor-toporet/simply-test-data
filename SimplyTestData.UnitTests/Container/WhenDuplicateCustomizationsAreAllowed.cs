using System.Linq;
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

            var customizations = Container.GetApplicableToType<C>().ToList();
            customizations.ForEach(act => act(c));

            customizations.Should().HaveCount(1);
            c.M.Should().Be(21);

            var flattenedInvocations = customizations.SelectMany(a => a.GetInvocationList()).ToArray();
            flattenedInvocations.Should().HaveCount(3);
            flattenedInvocations.Should().OnlyContain(f => f.Equals(IncreaseMemberBySeven));
        }
    }
}
