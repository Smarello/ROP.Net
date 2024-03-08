using ROP.Net.Extensions;
using System.Reflection.Metadata.Ecma335;

namespace ROP.Net.Test
{
    public class ThenTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestThatCallingGatherPeelCutIHaveACutApple()
        {
            var apple = new Apple();
            var peelApple = (Apple apple) => new PeeledApple();
            var cutApple = (PeeledApple apple) => new CutApple();

            var rail = apple.ToRail<Apple, Exception>()
                .Then(peelApple.ToRail())
                .Then(cutApple.ToRail());
           

            Assert.IsInstanceOf<CutApple>(rail.Result);
        }

        private class Apple {
        }
        private class PeeledApple {
        }
        private class CutApple { }
    }
}