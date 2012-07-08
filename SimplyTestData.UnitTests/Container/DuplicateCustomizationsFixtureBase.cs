using System;

namespace SimplyTestData.UnitTests.Container
{
    public abstract class DuplicateCustomizationsFixtureBase : StandardCustomizationsContainerFixtureBase
    {
        protected class C
        {
            public int M;
        }

        protected static readonly Action<C> IncreaseMemberBySeven = c => c.M += 7;
    }
}
