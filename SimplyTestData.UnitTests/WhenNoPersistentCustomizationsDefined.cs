using System;
using SampleDomain.Model;
using Xunit;

namespace SimplyTestData.UnitTests
{
    public class WhenNoPersistentCustomizationsDefined
    {
        public WhenNoPersistentCustomizationsDefined()
        {
            TestData.ClearPermanentCustomizations();
        }

        [Fact]
        public void CreatedObjectIsNaked()
        {
            var room = TestData.Create<Room>();

            Assert.Equal(default(int), room.Capacity);
            Assert.Equal(default(string), room.Location);
            Assert.Equal(default(string), room.Name);
        }

        [Fact]
        public void CreatingObjectWithSingleAdhocCustomizationProducesObjectWithThatCustomizationApplied()
        {
            const int capacity = 50;
            Action<Room> ofCapacity50 = r => r.Capacity = capacity;

            var room = TestData.Create(ofCapacity50);

            Assert.Equal(capacity, room.Capacity);
        }

        [Fact]
        public void CreatingObjectWithTwoAdhocCustomizationsProducesObjectWithBothCustomizationsApplied()
        {
            const string name = "Small Gym";
            Action<Room> smallGym = r => r.Name = name;
            const int capacity = 50;
            Action<Room> ofCapacity50 = r => r.Capacity = capacity;

            var room = TestData.Create(smallGym, ofCapacity50);

            Assert.Equal(name, room.Name);
            Assert.Equal(capacity, room.Capacity);
        }
    }
}
