using System;

namespace SimplyTestData.UnitTests.Container
{
    public abstract class ScopedApplicableCustomizationsFixtureBase : StandardCustomizationsContainerFixtureBase
    {
        protected static readonly Action<B> TransformBaseType = b => b.S = "foo";
        protected static readonly Action<C> TransformConcreteType = c => c.S = "bar";
        protected static readonly Action<I> TransformImplementedInterface = i => i.N = int.MaxValue;

        protected ScopedApplicableCustomizationsFixtureBase()
        {
            Container.AddForType(TransformBaseType);
            Container.AddForType(TransformConcreteType);
            Container.AddForType(TransformImplementedInterface);
        }

        protected class B
        {
            public string S;
        }

        protected interface I
        {
            int N { get; set; }
        }

        protected class C : B, I
        {
            public int N { get; set; }
        }
    }
}
