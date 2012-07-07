using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace SimplyTestData.UnitTests.Container
{
    public class WhenApplicableCustomizationsAreScopedToRequestedTypeOnly : StandardCustomizationsContainerFixtureBase
    {
        private class B
        {
            public string S;
        }

        private interface I
        {
            int N { get; set; }
        }

        private class C : B, I
        {
            public int N { get; set; }
        }

        public WhenApplicableCustomizationsAreScopedToRequestedTypeOnly()
        {
            Container.ScopeApplicableCustomizationsToRequestedTypeOnly = true;
        }

        [Fact]
        public void BaseClassCustomizationsAreNotApplied()
        {
            Action<B> foo = b => b.S = "foo";
            Container.AddForType<B>(foo);
            Action<C> bar = c => c.S = "bar";
            Container.AddForType<C>(bar);

            var applicableCustomizations = Container.GetApplicableToType<C>().ToArray();

            applicableCustomizations.Should().NotContain(foo);
            applicableCustomizations.Should().Contain(bar);
        }

        [Fact]
        public void CustomizationsForImplementedInterfacesAreNotApplied()
        {
            Action<I> foo = i => i.N = int.MinValue;
            Container.AddForType<I>(foo);
            Action<C> bar = c => c.N = int.MaxValue;
            Container.AddForType<C>(bar);

            var applicableCustomizations = Container.GetApplicableToType<C>().ToArray();

            applicableCustomizations.Should().NotContain(foo);
            applicableCustomizations.Should().Contain(bar);
        }
    }
}
