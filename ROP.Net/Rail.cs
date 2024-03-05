using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROP.Net
{
    public record Rail<TSuccess, TFailure> : IRail<TSuccess, TFailure>
    {
        public ITrack<TSuccess>? Success { get; init; }
        public ITrack<TFailure>? Error { get; init; }
        public bool IsSuccess { get; init; }

        public static IRail<TSuccess, TFailure> FromSuccessfulTrack(TSuccess successResult) => new Rail<TSuccess, TFailure>
        {
            IsSuccess = true,
            Success = new Success<TSuccess>(successResult)
        };

        public static IRail<TSuccess, TFailure> FromErrorTrack(TFailure error) => new Rail<TSuccess, TFailure>
        {
            IsSuccess = false,
            Error = new Failure<TFailure>(error)
        };

    }
}
