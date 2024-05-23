
namespace ROP.Net.Extensions
{
    public static class ThenExtension
    {
        public static IRail<TOut, TFailure> Then<TIn, TOut, TFailure>
            (this IRail<TIn, TFailure> prevResult, Func<IRail<TIn, TFailure>, IRail<TOut, TFailure>> doWork)
            => prevResult.IsSuccess switch
            {
                true => doWork(prevResult),
                false => Rail<TOut, TFailure>.FromErrorTrack(prevResult.Error!)
            };

        public static IRail<TOut, TFailure> Then<TIn, TOut, TFailure>
            (this IRail<TIn, TFailure> prevResult, Func<TIn?, IRail<TOut, TFailure>> doWork)
            => prevResult.Then(doWork.WithRailArguments());

        private static Func<IRail<TIn, TFailure>, IRail<TOut, TFailure>> WithRailArguments<TIn, TOut, TFailure>
            (this Func<TIn?, IRail<TOut, TFailure>> functionToBind)
            => (IRail<TIn, TFailure> input) => functionToBind(input.Result);
    }
}
