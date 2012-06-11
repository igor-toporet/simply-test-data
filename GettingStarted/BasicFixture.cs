// ReSharper disable InconsistentNaming

using System;
using SimplyTestData;
using Xunit;

namespace GettingStarted
{
    public class Greeting
    {
        public string Message { get; set; }
    }

    public class BasicFixture
    {
        private void HelloWorld(Greeting greeting)
        {
            greeting.Message = "Hello, World!";
        }

        [Fact]
        public void Example_01_HelloWorld()
        {
            var greeting = TestData.Create<Greeting>(HelloWorld);

            Assert.Equal("Hello, World!", greeting.Message);
        }

        [Fact]
        public void Example_02_GreetYourFriends()
        {
            var greeting = TestData.Create(Greeting("Hi"), To("Alice"), And("Bob"));

            Assert.Equal("Hi, Alice and Bob!", greeting.Message);
        }

        private Action<Greeting> Greeting(string something)
        {
            return greeting => greeting.Message = something;
        }

        private Action<Greeting> To(string friend)
        {
            return greeting => greeting.Message += ", " + friend;
        }

        private Action<Greeting> And(string friend)
        {
            return greeting => greeting.Message += " and " + friend + "!";
        }
    }
}

// ReSharper restore InconsistentNaming
