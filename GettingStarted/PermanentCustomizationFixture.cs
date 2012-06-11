// ReSharper disable InconsistentNaming

using System;
using SimplyTestData;
using Xunit;

namespace GettingStarted
{
    public interface IIdentifiable<TKey> where TKey : struct
    {
        TKey Id { get; set; }
    }

    public interface IVersionable
    {
        int Version { get; set; }
    }

    public class Resource : IIdentifiable<int>, IVersionable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }

    public class PermanentCustomizationFixture
    {
        public PermanentCustomizationFixture()
        {
            TestData.SetPermanentCustomizations<IVersionable>(v => v.Version = RandomInRange(1, 9));
            TestData.SetPermanentCustomizations<IIdentifiable<int>>(i => i.Id = RandomInRange(100, 999));
        }

        private static readonly Random _rnd = new Random();

        private int RandomInRange(int min, int max)
        {
            return _rnd.Next(min, max);
        }

        [Fact]
        public void Example_03_PermanentCustomizationsDemo()
        {
            var resource = TestData.Create<Resource>();

            Assert.True(100 <= resource.Id && resource.Id <= 999);
            Assert.True(1 <= resource.Version && resource.Version <= 9);
        }
    }
}

// ReSharper restore InconsistentNaming
