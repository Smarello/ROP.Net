using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROP.Net
{
    public record Rail<TSuccess, TFailure> : IRail<TSuccess, TFailure>
    {
        public ISuccessTrack<TSuccess> Success { get; init; }
        public IFailureTrack<TFailure> Error { get; init; }
        public bool IsSuccess { get; init; }

        private Rail()
        { }

        public static IRail<TSuccess, TFailure> FromSuccessfulTrack(TSuccess success) 
            => FromSuccessfulTrack(new Success<TSuccess>(success));
        

        public static IRail<TSuccess, TFailure> FromSuccessfulTrack(ISuccessTrack<TSuccess> result) => new Rail<TSuccess, TFailure>
        {
            IsSuccess = true,
            Success = result,
            Error = new Failure<TFailure>(default)
        };

        public static IRail<TSuccess, TFailure> FromErrorTrack(TFailure error)
            => FromErrorTrack(new Failure<TFailure>(error));

        public static IRail<TSuccess, TFailure> FromErrorTrack(IFailureTrack<TFailure> error) => new Rail<TSuccess, TFailure>
        {
            IsSuccess = false,
            Error = error,
            Success = new Success<TSuccess>(default)
        };
    }
}
