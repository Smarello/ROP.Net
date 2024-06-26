using ROP.Net.Extensions;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace ROP.Net.Test
{
    public class ThenTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TestThatCallingThenAsyncWithUncompletedTaskItGetsExecuted()
        {
            Task<IRail<Apple, string>> CreateApple = new Task<IRail<Apple, string>>(() =>
            {
                Thread.Sleep(500);
                return new Apple().ToSuccessRail<Apple, string>();
            });

            async Task<IRail<PeeledApple, string>> PeelAppleAsync(IRail<Apple, string> apple)
            {
                await Task.Delay(500);
                return new PeeledApple().ToSuccessRail<PeeledApple, string>();
            }

            IRail<PeeledApple, string> peeledApple = await CreateApple.ThenAsync(PeelAppleAsync);

            Assert.IsTrue(peeledApple.IsSuccess);
            Assert.NotNull(peeledApple.Result);

        }

        [Test]
        public async Task TestThatCallingThenAsyncOnANonRailOnbjectGetsTheTaskExecuted()
        {
            var apple = new Apple();

            Func<Apple> CreateApple = () => new Apple();            

            async Task<IRail<PeeledApple, Exception>> PeelAppleAsync(IRail<Apple, Exception> apple)
            {
                await Task.Delay(500);
                return new PeeledApple().ToSuccessRail<PeeledApple, Exception>();
            }

            IRail<PeeledApple, Exception> peeledApple = await new object().
                ToSuccessRail<object, Exception>()
                .Then(CreateApple.ToRail())
                .ThenAsync(PeelAppleAsync);


            Assert.IsTrue(peeledApple.IsSuccess);
            Assert.NotNull(peeledApple.Result);

        }

        [Test]
        public async Task TestThenAsyncOnAFuncWithoutParameters()
        {
            var apple = new Apple();


            async Task<IRail<Apple, Exception>> CreateApple()
            {
                await Task.Delay(500);
                return new Apple().ToSuccessRail<Apple, Exception>();
            }

            async Task<IRail<PeeledApple, Exception>> PeelAppleAsync(IRail<Apple, Exception> apple)
            {
                await Task.Delay(500);
                return new PeeledApple().ToSuccessRail<PeeledApple, Exception>();
            }

            IRail<PeeledApple, Exception> peeledApple = await new object().
                ToSuccessRail<object, Exception>()
                .ThenAsync(CreateApple)
                .ThenAsync(PeelAppleAsync);


            Assert.IsTrue(peeledApple.IsSuccess);
            Assert.NotNull(peeledApple.Result);

        }

        [Test]
        public void TestThatCallingGatherPeelCutIHaveACutApple()
        {
            var apple = new Apple();
            var peelApple = (Apple apple) => new PeeledApple();
            var cutApple = (PeeledApple apple) => new CutApple();

            var rail = apple.ToSuccessRail<Apple, Exception>()
                .Then(peelApple.ToRail())
                .Then(cutApple.ToRail());
           

            Assert.IsInstanceOf<CutApple>(rail.Result);
        }

        private class Apple { }
        private class PeeledApple { }
        private class CutApple { }
    }
}