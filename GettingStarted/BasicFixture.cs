// ReSharper disable InconsistentNaming

using SimplyTestData;
using Xunit;

namespace GettingStarted
{
    public class BasicFixture
    {
        private class Greeting
        {
            public string Message { get; set; }
        }

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
    }
}

// ReSharper restore InconsistentNaming
