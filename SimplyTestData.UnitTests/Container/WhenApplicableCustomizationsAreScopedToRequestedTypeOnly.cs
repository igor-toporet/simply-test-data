using System.Linq;
using FluentAssertions;
using Xunit;

namespace SimplyTestData.UnitTests.Container
{
    public class WhenApplicableCustomizationsAreScopedToRequestedTypeOnly : ScopedApplicableCustomizationsFixtureBase
    {
        public WhenApplicableCustomizationsAreScopedToRequestedTypeOnly()
        {
            Container.ScopeApplicableCustomizationsToRequestedTypeOnly = true;
        }

        [Fact]
        public void CustomizationsForRequestedTypeAreApplied()
        {
            var applicableCustomizations = Container.GetApplicableToType<C>().ToArray();

            applicableCustomizations.Should().HaveCount(1);
            applicableCustomizations.Should().Contain(TransformConcreteType);
        }

        [Fact]
        public void CustomizationsForBaseClassAreNotApplied()
        {
            var applicableCustomizations = Container.GetApplicableToType<C>().ToArray();

            applicableCustomizations.Should().NotContain(TransformBaseType);
        }

        [Fact]
        public void CustomizationsForImplementedInterfacesAreNotApplied()
        {
            var applicableCustomizations = Container.GetApplicableToType<C>().ToArray();

            applicableCustomizations.Should().NotContain(TransformImplementedInterface);
        }
    }
}
