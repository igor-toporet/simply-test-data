using System.Linq;
using FluentAssertions;
using Xunit;

namespace SimplyTestData.UnitTests.Container
{
    public class WhenApplicableCustomizationsAreNotScopedToRequestedTypeOnly : StandardCustomizationsContainerFixtureBase
    {
        public WhenApplicableCustomizationsAreNotScopedToRequestedTypeOnly()
        {
            Container.ScopeApplicableCustomizationsToRequestedTypeOnly = false;
        }

        [Fact]
        public void CustomizationsForRequestedTypeAreApplied()
        {
            var applicableCustomizations = Container.GetApplicableToType<C>().ToArray();

            applicableCustomizations.Should().HaveCount(3);
            applicableCustomizations.Should().Contain(TransformConcreteType);
        }

        [Fact]
        public void CustomizationsForBaseClassAreApplied()
        {
            var applicableCustomizations = Container.GetApplicableToType<C>().ToArray();

            applicableCustomizations.Should().Contain(TransformBaseType);
        }

        [Fact]
        public void CustomizationsForImplementedInterfacesAreApplied()
        {
            var applicableCustomizations = Container.GetApplicableToType<C>().ToArray();

            applicableCustomizations.Should().Contain(TransformImplementedInterface);
        }
    }
}