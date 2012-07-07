using System;

namespace SimplyTestData.UnitTests.Container
{
    public abstract class StandardCustomizationsContainerFixtureBase
    {
        protected readonly StandardCustomizationsContainer Container = new StandardCustomizationsContainer();

        protected readonly Action<B> TransformBaseType = b => b.S = "foo";
        protected readonly Action<C> TransformConcreteType = c => c.S = "bar";
        protected readonly Action<I> TransformImplementedInterface = i => i.N = int.MaxValue;

        protected StandardCustomizationsContainerFixtureBase()
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
