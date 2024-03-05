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
            var peel = PeelThelApple;
            var cut = CutTheApple;

            var result = apple.AsSuccessRail<Apple, Exception>()
                .Then(peel.AdaptToRail())
                .Then(cut.AdaptToRail());
           

            Assert.IsInstanceOf<CutApple>(result.Success.GetResult());
        }

        private PeeledApple PeelThelApple (Apple apple) => new PeeledApple();
        private CutApple CutTheApple(PeeledApple apple) => new CutApple();

        private class Apple {
        }
        private class PeeledApple {
        }
        private class CutApple { }
    }
}