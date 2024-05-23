
namespace ROP.Net.Extensions
{
    public static class ThenAsyncExtension
    {
        public static async Task<IRail<TOut, TFailure>> ThenAsync<TIn, TOut, TFailure>
        (this Task<IRail<TIn, TFailure>> prevResult,
        Func<IRail<TIn, TFailure>, Task<IRail<TOut, TFailure>>> doWork,
        int timeoutMilliseconds = 5000)
        {
            await ExecutePrevTaskIfNotExecuted(prevResult, timeoutMilliseconds);
            return prevResult.Result.IsSuccess switch
            {
                true => await doWork(prevResult.Result),
                false => Rail<TOut, TFailure>.FromErrorTrack(prevResult.Result.Error!)
            };
        }

        public static async Task<IRail<TOut, TFailure>> ThenAsync<TIn, TOut, TFailure>
            (this IRail<TIn, TFailure> prevResult,
            Func<IRail<TIn, TFailure>, Task<IRail<TOut, TFailure>>> doWork,
            int timeoutMilliseconds = 5000)
            => await Task.FromResult(prevResult).ThenAsync(doWork, timeoutMilliseconds);


        public static async Task<IRail<TOut, TFailure>> ThenAsync<TIn, TOut, TFailure>
        (this Task<IRail<TIn, TFailure>> prevResult,
        Func<Task<IRail<TOut, TFailure>>> doWork,
        int timeoutMilliseconds = 5000)
        {
            await ExecutePrevTaskIfNotExecuted(prevResult, timeoutMilliseconds);
            return prevResult.Result.IsSuccess switch
            {
                true => await doWork(),
                false => Rail<TOut, TFailure>.FromErrorTrack(prevResult.Result.Error!)
            };

        }

        public static async Task<IRail<TOut, TFailure>> ThenAsync<TIn, TOut, TFailure>
            (this IRail<TIn, TFailure> prevResult,
            Func<Task<IRail<TOut, TFailure>>> doWork,
            int timeoutMilliseconds = 5000)
            => await Task.FromResult(prevResult).ThenAsync(doWork, timeoutMilliseconds);


        static async Task ExecutePrevTaskIfNotExecuted<TIn, TFailure>(Task<IRail<TIn, TFailure>> prevResult, int timeoutMilliseconds)
        {
            if (prevResult.Status == TaskStatus.Created)
                prevResult.Start();
            if (!prevResult.IsCompleted)
                await prevResult.WaitAsync(TimeSpan.FromSeconds(timeoutMilliseconds));
            if (!prevResult.IsCompleted)
                throw new TimeoutException($"The task was not completed within the specified timeout ({timeoutMilliseconds} ms)");
        }

    }
}
