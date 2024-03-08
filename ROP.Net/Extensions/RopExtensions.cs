using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROP.Net.Extensions
{
    public static class RopExtensions
    {
        public static IRail<TSuccessOut, TFailure> Then<TSuccessIn, TSuccessOut, TFailure>(this IRail<TSuccessIn, TFailure> prevResult, Func<IRail<TSuccessIn, TFailure>, IRail<TSuccessOut, TFailure>> doWork) 
            => prevResult.IsSuccess switch
            {
                true => doWork(prevResult),
                false => Rail<TSuccessOut, TFailure>.FromErrorTrack(prevResult.FailureTrack)
            };

        public static IRail<TSuccessOut, TFailure> Then<TSuccessIn, TSuccessOut, TFailure>(this IRail<TSuccessIn, TFailure> prevResult, Func<TSuccessIn, IRail<TSuccessOut, TFailure>> doWork)
            => prevResult.Then(doWork.WithRailArguments());

        private static Func<IRail<TSuccessIn, TFailure>, IRail<TSuccessOut, TFailure>> WithRailArguments<TSuccessIn, TSuccessOut, TFailure>(this Func<TSuccessIn, IRail<TSuccessOut, TFailure>> functionToBind)
            => (IRail<TSuccessIn, TFailure> input) => functionToBind(input.Result);



        public static IRail<TSuccess, TFailure> ToRail<TSuccess, TFailure>(this TSuccess input)
        {
            return Rail<TSuccess, TFailure>.FromSuccessfulTrack(input);
        }


        //public static Func<IRail<TSuccessIn, Exception>, IRail<TSuccessOut, Exception>> ToRail<TSuccessIn, TSuccessOut>(this Func<TSuccessIn, TSuccessOut> functionToMap)
        //    => (IRail<TSuccessIn, Exception> input) =>
        //        input.Then((TSuccessIn inputThen) =>
        //            {
        //                try
        //                {
        //                    return functionToMap(inputThen).ToRail<TSuccessOut, Exception>();
        //                }
        //                catch (Exception ex)
        //                {
        //                    return Rail<TSuccessOut, Exception>.FromErrorTrack(ex);
        //                }
        //            });

        public static Func<IRail<TSuccessIn, Exception>, IRail<TSuccessOut, Exception>> ToRail<TSuccessIn, TSuccessOut>(this Func<TSuccessIn, TSuccessOut> functionToMap)
          => (IRail<TSuccessIn, Exception> input) =>
              {
                  try
                  {
                      return functionToMap(input.Result).ToRail<TSuccessOut, Exception>();
                  }
                  catch (Exception ex)
                  {
                      return Rail<TSuccessOut, Exception>.FromErrorTrack(ex);
                  }
              };


        public static Func<IRail<TSuccess, Exception>, IRail<TSuccess, Exception>> ToRail<TSuccess>(this Action<TSuccess> functionToTee)
            => functionToTee.ToFunc().ToRail();


        private static Func<T, T> ToFunc<T>(this Action<T> functionToTee) =>
            (T input) =>
            {
                functionToTee(input);
                return input;
            };





    }
}
