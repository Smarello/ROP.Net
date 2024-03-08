using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROP.Net
{
    internal record Rail<TSuccess, TFailure> : IRail<TSuccess, TFailure>
    {
        public ISuccessTrack<TSuccess> SuccessTrack { get; init; }
        public IFailureTrack<TFailure> FailureTrack { get; init; }
        public bool IsSuccess { get; init; }

        public TSuccess Result => SuccessTrack.GetResult();

        public TFailure Error => FailureTrack.GetError();

        private Rail()
        { }

        public static IRail<TSuccess, TFailure> FromSuccessfulTrack(TSuccess success) 
            => FromSuccessfulTrack(new Success<TSuccess>(success));
        

        public static IRail<TSuccess, TFailure> FromSuccessfulTrack(ISuccessTrack<TSuccess> result) => new Rail<TSuccess, TFailure>
        {
            IsSuccess = true,
            SuccessTrack = result,
            FailureTrack = new Failure<TFailure>(default)
        };

        public static IRail<TSuccess, TFailure> FromErrorTrack(TFailure error)
            => FromErrorTrack(new Failure<TFailure>(error));

        public static IRail<TSuccess, TFailure> FromErrorTrack(IFailureTrack<TFailure> error) => new Rail<TSuccess, TFailure>
        {
            IsSuccess = false,
            FailureTrack = error,
            SuccessTrack = new Success<TSuccess>(default)
        };
    }
}
