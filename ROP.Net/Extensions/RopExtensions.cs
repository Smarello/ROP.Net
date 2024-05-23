
namespace ROP.Net.Extensions
{
    public static class RopExtensions
    {

        public static IRail<TSuccess, TFailure> ToSuccessRail<TSuccess, TFailure>(this TSuccess input) =>
        Rail<TSuccess, TFailure>.FromSuccessfulTrack(input);

        public static IRail<TSuccess, TFailure> ToFailureRail<TSuccess, TFailure>(this TFailure input) =>
        Rail<TSuccess, TFailure>.FromErrorTrack(input);


        public static Func<IRail<TSuccessIn, Exception>, IRail<TSuccessOut, Exception>> ToRail<TSuccessIn, TSuccessOut>
            (this Func<TSuccessIn, TSuccessOut> functionToMap)
          => (IRail<TSuccessIn, Exception> input) =>
              {
                  try
                  {
                      return functionToMap(input.Result!).ToSuccessRail<TSuccessOut, Exception>();
                  }
                  catch (Exception ex)
                  {
                      return Rail<TSuccessOut, Exception>.FromErrorTrack(ex);
                  }
              };


        public static Func<IRail<TSuccessIn, Exception>, IRail<TSuccessIn, Exception>> ToRail<TSuccessIn>(this Action<TSuccessIn> functionToTee)
            => functionToTee.ToFunc().ToRail();

        public static Func<IRail<TSuccessOut, Exception>> ToRail<TSuccessOut>(this Func<TSuccessOut> functionToMap)
            => () =>
            {
                try
                {
                    return functionToMap().ToSuccessRail<TSuccessOut, Exception>();
                }
                catch (Exception ex)
                {
                    return Rail<TSuccessOut, Exception>.FromErrorTrack(ex);
                }
            };


        private static Func<T, T> ToFunc<T>(this Action<T> functionToTee) =>
            (T input) =>
            {
                functionToTee(input);
                return input;
            };





    }
}
