﻿using System;
using SampleDomain;
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
        public void CreatingObjectWitAdhocCustomizationProducesProperlyCustomizedObject()
        {
            const int capacity = 50;
            Action<Room> ofCapacity50 = r => r.Capacity = capacity;

            var room = TestData.Create(ofCapacity50);

            Assert.Equal(capacity, room.Capacity);
        }
    }
}